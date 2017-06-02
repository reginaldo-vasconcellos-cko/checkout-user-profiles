using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Api.Helpers;
using UserProfiles.Api.Models;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> VerifyExistsAsync(string name)
        {
            return await _roleManager.RoleExistsAsync(name);
        }

        public async Task<List<Role>> GetAsync()
        {
            var identityRoles = _roleManager.Roles.ToList();

            var roles = new List<Role>();

            await identityRoles.ForEachAsync(async role =>
            {
                var identityClaims = await _roleManager.GetClaimsAsync(role);

                roles.Add(new Role
                {
                    Name = role.Name,
                    Claims = identityClaims
                        .Select(c => new ClaimBase { Type = c.Type, Value = c.Value })
                        .ToList()
                });
            });

            return roles;
        }

        public async Task CreateAsync(string name)
        {
            var role = new IdentityRole { Name = name };

            await _roleManager.CreateAsync(role);
        }

        public async Task AssignClaimAsync(AssignClaimToRoleRequest request)
        {
            var role = await _roleManager.FindByNameAsync(request.Role);

            foreach (var claim in request.Claims)
                await _roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
        }
    }
}
