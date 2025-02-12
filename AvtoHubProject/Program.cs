using AvtoHubProject.AuthorizationServices.AuthPolicyForUsers;
using AvtoHubProject.AuthorizationServices.BannedUsersAuthorize;
using AvtoHubProject.Models;
using AvtoHubProject.Services;
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
builder.Services.AddSingleton<RegisterService>();
builder.Services.AddSingleton<LoginService>();
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
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Lockout.MaxFailedAccessAttempts = 2;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});
builder.Services.AddAuthorizationBuilder().AddPolicy("OnlyUsersCanExecute", policy =>
{
    policy.AddRequirements(new OnlyUsersAuthorize());
}).AddPolicy("RestrictBannedUsersFromExecuting", policy =>
{
    policy.AddRequirements(new BannedUsers());
}).AddPolicy("OnlyAdminsCanExecute", policy =>
{
    policy.RequireClaim("IsAdmin", "true");
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error");
    app.UseHsts();
}
app.UseStaticFiles();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
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
