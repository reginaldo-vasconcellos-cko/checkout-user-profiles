using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UserProfiles.Common.Models.Requests;
using UserProfiles.Data.Repository;
using UserProfiles.WebApi.Security.Requirements;

namespace UserProfiles.WebApi.Security.Handlers
{
    public class ResourceAccessHandler : AuthorizationHandler<ResourceAccessRequirement>
    {
        private readonly IUserResouceIdentityRepository _userResouceIdentityRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ResourceAccessHandler(IUserResouceIdentityRepository userResouceIdentityRepository, 
            IUserRepository userRepository, 
            UserManager<IdentityUser> userManager)
        {
            _userResouceIdentityRepository = userResouceIdentityRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAccessRequirement requirement)
        {
            var userIdendity = await _userManager.FindByNameAsync(context.User.Identity.Name);
            var user = _userRepository.GetByRefId(userIdendity.Id);

            var resourcePermission = new VerifyUserResouceIdentityPermissionRequest
            {
                UserId = user.Id,
                IdentityType = (int)requirement.IdentityType,
                IdentityId = requirement.Id
            };

            if (_userResouceIdentityRepository.VerifyUserResouceIdentityPermission(resourcePermission))
                context.Succeed(requirement);
        }
    }
}
