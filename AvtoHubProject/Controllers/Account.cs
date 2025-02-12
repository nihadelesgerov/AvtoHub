using AvtoHubProject.Models;
using AvtoHubProject.Services;
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
        private readonly RegisterService registerService;
        private readonly Logger<Account> logger;
        private readonly LoginService loginService;

        public Account(AvtoHubDbContext context,UserManager<AvtoHubUser> userManager,SignInManager<AvtoHubUser> signInManager,RoleManager<IdentityRole> roleManager,RegisterService registerService,Logger<Account> logger,LoginService loginService)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.registerService = registerService;
            this.logger = logger;
            this.loginService = loginService;
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
              var result = await registerService.RegisterUser(modelReg);
                if (result.Succeeded)
                {
                    return RedirectToAction("HomePage","AvtoHub");
                }
                ModelState.AddModelError("RegisterFailed", "Something went wrong while process");
                logger.LogWarning($"User with email {modelReg.Email} failed to register");
                return View(modelReg);
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
                var result = await loginService.LoginUser(logmodel);
                if(result.Succeeded)
                {
                    logger.LogInformation($"User with email {logmodel.Email} logged in ");
                    return RedirectToAction("HomePage", "AvtoHub");
                }
                else
                {
                    logger.LogInformation($"User with email  {logmodel.Email} failed to login");
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
        // SignOut method overrides ControllerBase SigOut, that may cause error but it's late to change every name in Razor Pages and others (Because I didn't use Layouts)
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
