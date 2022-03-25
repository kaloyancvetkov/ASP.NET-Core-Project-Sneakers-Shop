using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SneakersShop.Data;
using SneakersShop.Infrastructure;
using SneakersShop.Services.Sneakers;
using SneakersShop.Services.Statistics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SneakersShopDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<SneakersShopDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<ISneakersService, SneakersService>();

var app = builder.Build();

app.PrepareDatabase();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection().
    UseStaticFiles().
    UseRouting().
    UseAuthentication()
    .UseAuthorization().
    UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapRazorPages();
    });




app.Run();


