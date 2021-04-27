using System.Threading.Tasks;
using Auth.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Security
{
    public class SuperAdminRoleRequirement : IAuthorizationRequirement
    {

    }

    public class SuperAdminRoleHandler : AuthorizationHandler<SuperAdminRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            SuperAdminRoleRequirement requirement)
        {
            if (context.User.IsInRole(Constants.SuperAdmin))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}