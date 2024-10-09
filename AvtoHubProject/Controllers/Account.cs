using AvtoHubProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AvtoHubProject.Controllers
{
    public class Account : Controller
    {
        private readonly AvtoHubDbContext context;
        private readonly UserManager<AvtoHubUser> userManager;
        private readonly SignInManager<AvtoHubUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public Account(AvtoHubDbContext context,UserManager<AvtoHubUser> userManager,SignInManager<AvtoHubUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [AutoValidateAntiforgeryToken]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(AvtoHubRegisterModel modelReg)
        {

            if (ModelState.IsValid)
            {
                var user = new AvtoHubUser
                {
                    UserName=modelReg.Email,
                    Email = modelReg.Email
                };
                var result = await userManager.CreateAsync(user, modelReg.Password);

                if (result.Succeeded)
                {
                    //if(modelReg.Email=="testadmin@gmail.com" && modelReg.Password == "Admin123#")
                    //{
                    //    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    //    await userManager.AddToRoleAsync(user, "Admin");
                    //    await signInManager.SignInAsync(user, true, null);
                    //    return RedirectToAction("AdminDashboard", "AdminHub");
                    //}
                    if (! await roleManager.RoleExistsAsync("User"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("User"));
                        await userManager.AddToRoleAsync(user, "User");
                        await signInManager.SignInAsync(user, true, null);
                        return RedirectToAction("HomePage", "AvtoHub");
                    }
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, true, null);
                    return RedirectToAction("HomePage", "AvtoHub");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("RegisterFailed", error.Description);
                    return View(modelReg);
                }

            }
            return View(modelReg);
        }
        [AutoValidateAntiforgeryToken]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(AvtoHubLoginModel logmodel)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(logmodel.Email, logmodel.Password,isPersistent:true,lockoutOnFailure:true);
                if(result.Succeeded)
                {
                    return RedirectToAction("HomePage", "AvtoHub");
                }
                else if (result.IsLockedOut)
                {
                    return RedirectToAction("LockOut");
                }
                else
                {
                    ModelState.AddModelError("LoginFailed", "E-Poçt adresi və ya şifrə yanlışdır");
                    return View(logmodel);
                }

            }
            return View(logmodel);

        }
        public IActionResult LockOut()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("HomePage", "AvtoHub");
        }

        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}
