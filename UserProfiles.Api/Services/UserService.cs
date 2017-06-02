using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Api.Helpers;
using UserProfiles.Api.Models;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Models.Responses;
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

        public async Task CreateAsync(CreateAccountRequest request)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email
                };

                //create the identity user 
                var result = await _userManager.CreateAsync(newUser);

                if (!result.Succeeded)
                    return;

                var user = await _userManager.FindByNameAsync(newUser.Email);

                if (request.Roles.Any())
                {
                    await request.Roles.ForEachAsync(async role =>
                    {
                        if(await _roleManager.RoleExistsAsync(role))
                            await _userManager.AddToRoleAsync(user, role);
                    });
                }

                if (request.Claims.Any())
                {
                    await request.Claims.ForEachAsync(async claim =>
                    {
                        await _userManager.AddClaimAsync(user, new Claim("feature", claim));
                    });
                }

                //create the user locally
                _userRepository.Add(new User { GuidRef = user.Id });
            }
            catch (Exception e)
            {
                throw e;
            }

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

        public async Task<GetUserPermissionsResponse> GetDetailsByIdAsync(int id)
        {
            var result =  await _userRepository.GetDetailsByIdAsync(id);

            return result.FirstOrDefault();
        }

        public async Task<List<GetUserPermissionsResponse>> GetDetailsAsync()
        {
            return await _userRepository.GetDetailsByIdAsync();
        }

        public async Task<List<Claim>> GetClaimsByUserNameAsync(string userName)
        {
            var userIdentity = await _userManager.FindByNameAsync(userName);

            return await GetClaimsByUserIdentity(userIdentity);
        }

        public async Task<List<ClaimBase>> GetClaimsByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var userIdentity = await _userManager.FindByIdAsync(user.GuidRef);

            var claims = await GetClaimsByUserIdentity(userIdentity);

            return claims.Select(claim => new ClaimBase { Type = claim.Type, Value = claim.Value }).ToList();
        }

        public async Task<IList<string>> GetRolesByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var userIdentity = await _userManager.FindByIdAsync(user.GuidRef);

            return await _userManager.GetRolesAsync(userIdentity);
        }

        private async Task<List<Claim>> GetClaimsByUserIdentity(IdentityUser userIdentity)
        {
            var roleNames = await _userManager.GetRolesAsync(userIdentity);

            var roles = new List<IdentityRole>();

            foreach (var roleName in roleNames)
                roles.Add(await _roleManager.FindByNameAsync(roleName));

            var claims = new List<Claim>();

            foreach (var role in roles)
                claims.AddRange(await _roleManager.GetClaimsAsync(role));

            claims.AddRange(await _userManager.GetClaimsAsync(userIdentity));

            var distinct = claims.GroupBy(c => new {c.Type, c.Value})
                                 .Select(grp => grp.First())
                                 .ToList();
            return distinct;
        }
    }
}
