using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UserProfiles.Api.Models;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Services
{
    public interface IRoleService
    {
        Task<bool> VerifyExistsAsync(string role);

        Task<List<Role>> GetAsync();

        Task CreateAsync(string role);

        Task AssignClaimAsync(AssignClaimToRoleRequest request);
    }
}
