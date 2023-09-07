using HR_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace HR_System.CustomAuthorization
{
    public class GroupRequirement : IAuthorizationRequirement
    {
        public string RequiredGroup { get; }

        public GroupRequirement(string requiredGroup)
        {
            RequiredGroup = requiredGroup;
        }
    }

    public class GroupAuthorizationHandler : AuthorizationHandler<GroupRequirement, ApplicationUser>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupRequirement requirement, ApplicationUser resource)
        {
            // Check if the user belongs to the required group
            if (resource.Group != null && resource.Group.Name == requirement.RequiredGroup)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
