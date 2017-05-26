using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UserProfiles.Api.Models;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Models.Responses;
using UserProfiles.Api.Repository;
using UserProfiles.Api.Security.Attributes;

namespace UserProfiles.Api.Controllers.Profile
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IUserResouceIdentityRepository _userResourceIdentityRepository;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, 
                            IUserRepository userRepository, IUserResouceIdentityRepository userResourceIdentityRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _userResourceIdentityRepository = userResourceIdentityRepository;
        }

        [HttpPost]
        [RequirePermission("user.register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccountRequest request)
        {
            var newUser = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            //create the identity user 
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return BadRequest(ModelState);
            }

            var user = _userManager.FindByNameAsync(newUser.Email).Result;

            //create the user locally
            _userRepository.Add(new User { GuidRef = user.Id });

            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole { Name = "User" };

                var roleResult = _roleManager.CreateAsync(role).Result;

                if (!roleResult.Succeeded)
                    ModelState.AddModelError("", "Error while creating role!");
            }

            _userManager.AddToRoleAsync(newUser, "User").Wait();

            return Ok();
        }

        [HttpPost]
        [RequirePermission("user.assignClaim")]
        public async Task<IActionResult> AssignClaim([FromBody] AssignClaimToUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            await _userManager.AddClaimAsync(user, new Claim(request.Claim.Type, request.Claim.Value));

            return Ok();
        }

        [HttpPost]
        [RequirePermission("user.assignRole")]
        public async Task<IActionResult> AssignRole([FromBody]AssignRoleToUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "User does not exist!");

                return BadRequest(ModelState);
            }

            if (!_roleManager.RoleExistsAsync(request.Role).Result)
            {
                ModelState.AddModelError("", "Role does not exist!");

                return BadRequest(ModelState);
            }

            _userManager.AddToRoleAsync(user, request.Role).Wait();

            return Ok();
        }

        [HttpPost]
        [RequirePermission("user.assignResource")]
        public async Task<IActionResult> AssignResource([FromBody]AssignResourceToUserRequest request)
        {
            _userResourceIdentityRepository.InsertUserResouceIdentity(request);

            return Ok();
        }

        [HttpGet("{id}")]
        [RequirePermission("user.get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_userRepository.GetById(id));
        }
    }
}
