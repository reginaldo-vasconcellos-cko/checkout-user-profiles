using System.Threading.Tasks;
using UserProfiles.Api.Models;

namespace UserProfiles.Api.Services
{
    public interface IRoleService
    {
        Task<bool> VerifyExistsAsync(string role);

        Task CreateAsync(string role);

        Task AssignClaimAsync(AssignClaimToRoleRequest request);
    }
}
