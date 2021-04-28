using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MvcApp.Utilities;

namespace MvcApp.Security
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
