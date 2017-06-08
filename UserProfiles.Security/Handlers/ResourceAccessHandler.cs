using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using UserProfiles.Api.Services;
using UserProfiles.Common.Models.Requests;
using UserProfiles.Security.Requirements;

namespace UserProfiles.Security.Handlers
{
    public class ResourceAccessHandler : AuthorizationHandler<ResourceAccessRequirement>
    {
        private readonly IUserResourceIdentityService _userResouceIdentityService;

        private readonly IUserService _userService;

        public ResourceAccessHandler(IUserResourceIdentityService userResouceIdentityService, 
            IUserService userService)
        {
            _userResouceIdentityService = userResouceIdentityService;
            _userService = userService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAccessRequirement requirement)
        {
            var user = await _userService.GetByNameAsync(context.User.Identity.Name);

            var resourcePermission = new VerifyUserResouceIdentityPermissionRequest
            {
                UserId = user.Id,
                IdentityType = (int)requirement.IdentityType,
                IdentityId = requirement.Id
            };

            var flag = _userResouceIdentityService.VerifyUserResouceIdentityPermission(resourcePermission);

            if (flag)
                context.Succeed(requirement);
            else
                context.Fail();
        }
    }
}
