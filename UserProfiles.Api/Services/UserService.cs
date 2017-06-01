using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Api.Models;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Repository;

namespace UserProfiles.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IUserResouceIdentityRepository _userResouceIdentityRepository;

        public UserService(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository,
            IUserResouceIdentityRepository userResouceIdentityRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _userResouceIdentityRepository = userResouceIdentityRepository;
        }

        public async Task RegisterAsync(RegisterAccountRequest request)
        {
            try
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
                    //foreach (var error in result.Errors)
                    //    ModelState.AddModelError(string.Empty, error.Description);
                }

                var user = await _userManager.FindByNameAsync(newUser.Email);

                //create the user locally
                _userRepository.Add(new User { GuidRef = user.Id });
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<List<Claim>> GetClaimsByUserNameAsync(string userName)
        {
            var userIdentity = await _userManager.FindByNameAsync(userName);
            var roleNames = await _userManager.GetRolesAsync(userIdentity);

            var roles = new List<IdentityRole>();

            foreach (var roleName in roleNames)
                roles.Add(await _roleManager.FindByNameAsync(roleName));

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

            foreach (var role in roles)
                claims.AddRange(await _roleManager.GetClaimsAsync(role));

            return claims;
        }

        public async Task AssignClaimAsync(AssignClaimToUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            await _userManager.AddClaimAsync(user, new Claim(request.Claim.Type, request.Claim.Value));
        }

        public async Task AssignRoleAsync(AssignRoleToUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            //if (user == null)
            //{
            //    ModelState.AddModelError("", "User does not exist!");

            //    return BadRequest(ModelState);
            //}

            //if (!await _roleManager.RoleExistsAsync(request.Role))
            //{
            //    ModelState.AddModelError("", "Role does not exist!");

            //    return BadRequest(ModelState);
            //}

            await _userManager.AddToRoleAsync(user, request.Role);
        }

        public void AssignResource(AssignResourceToUserRequest request)
        {
            foreach (var resourceId in request.ResourceIdentityId)
                _userResouceIdentityRepository.InsertUserResouceIdentity(request.UserId, resourceId);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            return _userRepository.GetByRefId(user.Id);
        }
    }
}
