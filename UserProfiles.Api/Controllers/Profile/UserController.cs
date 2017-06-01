using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Api.Models;
using UserProfiles.Api.Models.Enums;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Security.Attributes;
using UserProfiles.Api.Security.Requirements;
using UserProfiles.Api.Services;

namespace UserProfiles.Api.Controllers.Profile
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IUserService userService, IAuthorizationService authorizationService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [RequirePermission("user.register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccountRequest request)
        {
            try
            {
                await _userService.RegisterAsync(request);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPost]
        [RequirePermission("user.assignClaim")]
        public async Task<IActionResult> AssignClaim([FromBody] AssignClaimToUserRequest request)
        {
            await _userService.AssignClaimAsync(request);

            return Ok();
        }

        [HttpPost]
        [RequirePermission("user.assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleToUserRequest request)
        {
            var requirements = new List<ResourceAccessRequirement>
            {
                new ResourceAccessRequirement(request.UserId, IdentityType.User),
                new ResourceAccessRequirement(request.RoleId, IdentityType.Role)
            };

            if (!await _authorizationService.AuthorizeAsync(User, null, requirements))
                return new ChallengeResult();

            await _userService.AssignRoleAsync(request);

            return Ok();
        }

        [HttpPost]
        [RequirePermission("user.assignResource")]
        public async Task<IActionResult> AssignResource([FromBody]AssignResourceToUserRequest request)
        {
            _userService.AssignResource(request);

            return Ok();
        }

        //[HttpGet("{id}")]
        //[RequirePermission("user.get")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    return Ok(_userRepository.GetById(id));
        //}
    }
}
