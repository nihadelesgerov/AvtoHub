using AvtoHubProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AvtoHubProject.Services
{
    public class RegisterService
    {
        private readonly SignInManager<AvtoHubUser> signInManager;
        private readonly UserManager<AvtoHubUser> userManager;
        private readonly ILogger<RegisterService> logRegiserService;

        public RegisterService(SignInManager<AvtoHubUser> signInManager,UserManager<AvtoHubUser> userManager,ILogger<RegisterService>logRegiserService) 
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logRegiserService = logRegiserService;
        }
        public async Task<IdentityResult> RegisterUser(AvtoHubRegisterModel model)
        {
            var user = new AvtoHubUser
            {
                UserName = model.Email,
                Email = model.Email,
            };
            var create = userManager.CreateAsync(user);
            string CurrentRegisterDate = Convert.ToString(DateTime.UtcNow);
            var claims = new List<Claim>()
                {
                    new Claim("AvtoHubUser","DefaultUser"),
                    new Claim("Email",model.Email),
                    new Claim("RegisterDate",CurrentRegisterDate),
                    new Claim("IsBanned","false"),
                    new Claim("IsAdmin","false"),
                    new Claim("IsDefaultUser","true")
                };
                await userManager.AddClaimsAsync(user, claims);
                await signInManager.SignInAsync(user, isPersistent: true);
                logRegiserService.LogInformation($"User with email :{model.Email} has already registered Succesfully at {CurrentRegisterDate}");
                return IdentityResult.Success;
        }
    }
}
