using Microsoft.AspNetCore.Authorization;

namespace AvtoHubProject.AuthorizationServices.AuthPolicyForUsers
{
    public class OnlyUsersAuthorizeHandler : AuthorizationHandler<OnlyUsersAuthorize>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyUsersAuthorize requirement)
        {
            if (context.User.HasClaim("IsDefaultUser", "true"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
