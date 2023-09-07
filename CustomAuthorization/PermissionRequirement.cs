using HR_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace HR_System.CustomAuthorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; }

        public PermissionRequirement(string requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }
    }

    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);
            if (user != null && user.Group != null)
            {
                var hasPermission = user.Group.permissions != null && user.Group.permissions.Any(p => p.pageName == requirement.RequiredPermission);
                if (hasPermission)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail();
            await Task.CompletedTask; // Return a completed Task
        }
    }
}
