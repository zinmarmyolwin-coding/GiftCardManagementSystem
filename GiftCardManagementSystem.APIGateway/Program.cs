using GiftCardManagementSystem.APIGateway.Service;
using GiftCardManagementSystem.Infrastructure.AppDbContextModels;
using GiftCardManagementSystem.Repository.IRepository;
using GiftCardManagementSystem.Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Add DB Context

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetSection("ConnectionStrings:DbConnection").Value;
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);

#endregion

#region Configure RabbitMQ Connection

var connectionUrl = configuration["RabbitMq:RabbitMqConnectionString"];
var factory = new ConnectionFactory { Uri = new Uri(connectionUrl) };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
builder.Services.AddSingleton(new RabbitMqConcurrencyService(channel, maxConcurrentRequests: 5));

#endregion

builder.Services.AddScoped<ApiGateWayService>();
builder.Services.AddScoped<IGiftCardRepository, GiftCardRepository>();

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };

        // Configure events for refresh token handling
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
