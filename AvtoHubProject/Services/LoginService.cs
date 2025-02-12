using AvtoHubProject.Models;
using Microsoft.AspNetCore.Identity;

namespace AvtoHubProject.Services
{
    public class LoginService
    {
        private readonly SignInManager<AvtoHubUser> signInManager;

        public LoginService(SignInManager<AvtoHubUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<SignInResult> LoginUser(AvtoHubLoginModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: true);
            return result;
        }
    }
}
