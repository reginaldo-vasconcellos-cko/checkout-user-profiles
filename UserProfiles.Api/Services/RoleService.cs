using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Api.Models;

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

        public async Task CreateAsync(string name)
        {
            var role = new IdentityRole { Name = name};

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
