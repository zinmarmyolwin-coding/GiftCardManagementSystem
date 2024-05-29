using GiftCardManagementSystem.Admin.Features.Admin;
using GiftCardManagementSystem.Admin.Features.GiftCard;
using GiftCardManagementSystem.Admin.Features.Payment;
using GiftCardManagementSystem.Admin.Features.User;
using GiftCardManagementSystem.Infrastructure.AppDbContextModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

#region Add DB Context

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetSection("ConnectionStrings:DbConnection").Value;
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);

#endregion

builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<GiftCardService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<UserService>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=GiftCard}/{action=GiftCardList}");

app.Run();
