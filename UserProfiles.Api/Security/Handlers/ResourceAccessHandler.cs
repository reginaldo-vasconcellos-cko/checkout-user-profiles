using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Repository;
using UserProfiles.Api.Security.Requirements;

namespace UserProfiles.Api.Security.Handlers
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

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAccessRequirement requirement)
        {
            var userIdendity = _userManager.FindByNameAsync(context.User.Identity.Name).Result;
            var user = _userRepository.GetByRefId(userIdendity.Id);

            var resourcePermission = new VerifyUserResouceIdentityPermissionRequest
            {
                UserId = user.Id,
                IdentityType = (int)requirement.IdentityType,
                IdentityId = requirement.Id
            };

            if (_userResouceIdentityRepository.VerifyUserResouceIdentityPermission(resourcePermission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
