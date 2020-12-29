using Auth.Utilities;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Auth.Security
{
    public class SuperAdminHandler :
    AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ManageAdminRolesAndClaimsRequirement requirement)
        {
            if (context.User.IsInRole(Constants.SuperAdmin))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
