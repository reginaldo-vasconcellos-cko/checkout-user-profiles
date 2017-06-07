using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Common.Helpers;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Requests;

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

        public async Task<List<RoleDto>> GetAsync()
        {
            var identityRoles = _roleManager.Roles.ToList();

            var roles = new List<RoleDto>();

            await identityRoles.ForEachAsync(async role =>
            {
                var identityClaims = await _roleManager.GetClaimsAsync(role);

                roles.Add(new RoleDto
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

        public async Task CreateAsync(RoleDto role)
        {
            var identityRole = new IdentityRole { Name = role.Name };

            await _roleManager.CreateAsync(identityRole);

            if (role.Claims == null)
                return;

            identityRole = await _roleManager.FindByNameAsync(role.Name);

            await role.Claims.ForEachAsync(async claim =>
            {
                await _roleManager.AddClaimAsync(identityRole, new Claim(claim.Type, claim.Value));
            });
        }

        public async Task EditAsync(RoleDto role)
        {
            var identityRole = await _roleManager.FindByNameAsync(role.Name);

            var claims = await _roleManager.GetClaimsAsync(identityRole);

            //remove claims first
            if (claims != null)
            {
                await claims.ToList().ForEachAsync(async claim =>
                {
                    await _roleManager.RemoveClaimAsync(identityRole, claim);
                });
            }

            if (role.Claims == null)
                return;

            await role.Claims.ToList().ForEachAsync(async claim =>
            {
                await _roleManager.AddClaimAsync(identityRole, new Claim(claim.Type, claim.Value));
            });
        }

        public async Task AssignClaimAsync(AssignClaimToRoleRequest request)
        {
            var role = await _roleManager.FindByNameAsync(request.Role);

            foreach (var claim in request.Claims)
                await _roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
        }
    }
}
