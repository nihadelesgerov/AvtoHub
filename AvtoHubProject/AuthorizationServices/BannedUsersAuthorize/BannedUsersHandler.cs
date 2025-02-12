using Microsoft.AspNetCore.Authorization;

namespace AvtoHubProject.AuthorizationServices.BannedUsersAuthorize
{
    public class BannedUsersHandler : AuthorizationHandler<BannedUsers>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BannedUsers requirement)
        {
            if (context.User.HasClaim("IsBanned", "true"))
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
