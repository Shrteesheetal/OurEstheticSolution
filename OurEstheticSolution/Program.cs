using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OurEstheticSolution.Data;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models;
using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.Repository;
using OurEstheticSolution.Services;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IDBConnection, DBConnection>();
builder.Services.AddScoped<IEmployee, IEmployeeRepo>();
builder.Services.AddScoped<IProduct, IProductRepo>();
builder.Services.AddScoped<IService, IServiceRepo>();
builder.Services.AddScoped<IAppointment, IAppointmentRepo>();


builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.RequireUniqueEmail = true;


})
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();
var app = builder.Build();


await SeedService.SeedDatabase(app.Services);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
