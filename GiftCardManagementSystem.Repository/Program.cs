using GiftCardManagementSystem.Infrastructure.AppDbContextModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


#region Add DB Context

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetSection("ConnectionStrings:DbConnection").Value;
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);

#endregion

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run();
