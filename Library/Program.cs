// using Library.Areas.Identity.Data;
// using Library.Models;
// using Microsoft.EntityFrameworkCore;
//
// var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ??
//                        throw new InvalidOperationException(
//                            "Connection string 'ApplicationDbContextConnection' not found.");
//
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));
//
// builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
//     .AddEntityFrameworkStores<ApplicationDbContext>();
//
// // Add services to the container.
// builder.Services.AddControllersWithViews();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }
//
// app.UseHttpsRedirection();
// app.UseStaticFiles();
//
// app.UseRouting();
// app.UseAuthentication();
// ;
//
// app.UseAuthorization();
//
// app.MapControllerRoute(
//     "default",
//     "{controller=Home}/{action=Index}/{id?}");
//
// app.MapRazorPages();
//
// app.Run();

using Library.Areas.Identity.Data;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ??
                       throw new InvalidOperationException(
                           "Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authorization

AddAuthorizationPolicies(builder.Services);

#endregion

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Additional configuration for API CRUD
app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Books}/{action=Index}/{id?}");

app.Run();


void AddAuthorizationPolicies(IServiceCollection services)
{
    services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });
}
