using AvtoHubProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtoHubProject.Controllers
{
    [ValidateAntiForgeryToken]
    [AutoValidateAntiforgeryToken]
    [Authorize(policy: "OnlyAdminsCanExecute")]
    public class AdminHub : Controller
    {
        private readonly AvtoHubDbContext context;
        private readonly UserManager<AvtoHubUser> userManager;
        private readonly SignInManager<AvtoHubUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment environment;

        public AdminHub(AvtoHubDbContext context, UserManager<AvtoHubUser> userManager, SignInManager<AvtoHubUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.environment = environment;
        }

        public IActionResult AdminDashboard()
        {
            var list =  context.AvtoHubProducts.Include(a => a.AvtoHubUser).ToList();
            ViewBag.CountOfProduct= list.Count;
            return View(list);
        }
        public IActionResult AdminDashboardUsers()
        {
            var list = context.Users.ToList();

            return View(list);
        }
        public IActionResult AdminDashboardMessages()
        {
            return View();
        }
    }
}
