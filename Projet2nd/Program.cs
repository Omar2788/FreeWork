using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projet2nd.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionstring = builder.Configuration.GetConnectionString("con");
builder.Services.AddDbContext<Projet2ndContext>(options =>
    options.UseSqlServer(connectionstring));

builder.Services.AddDefaultIdentity<Projet2ndUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Projet2ndContext>();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=BuyAction}/{id?}");
app.MapRazorPages();
app.Run();
