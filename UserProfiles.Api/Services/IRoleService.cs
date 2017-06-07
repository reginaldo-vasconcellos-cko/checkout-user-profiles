using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Requests;

namespace UserProfiles.Api.Services
{
    public interface IRoleService
    {
        Task<bool> VerifyExistsAsync(string role);

        Task<List<RoleDto>> GetAsync();

        Task CreateAsync(string role);

        Task CreateAsync(RoleDto role);

        Task EditAsync(RoleDto role);

        Task AssignClaimAsync(AssignClaimToRoleRequest request);
    }
}
