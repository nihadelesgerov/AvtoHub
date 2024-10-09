using AvtoHubProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AvtoHubDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulDbConnectionOfAvtoHubProject"));
});
builder.Services.AddIdentity<AvtoHubUser, IdentityRole>().AddEntityFrameworkStores<AvtoHubDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccesDenied";
    options.LoginPath = "/Account/Login";
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;


    options.Lockout.MaxFailedAccessAttempts = 2;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});
builder.Services.AddAuthentication();   
//builder.Services.AddIdentity<>
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
    name: "default",
    pattern: "{controller=AvtoHub}/{action=HomePage}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
         name: "accountdetails",
        pattern: "Account/profile",
        defaults: new { controller = "AvtoHub", action = "MyAccount"});

    endpoints.MapControllerRoute(
        name: "addingproduct",
        pattern: "AvtoHub/add",
        defaults: new { controller = "AvtoHub", action = "AddProduct" });
});
app.Run();
