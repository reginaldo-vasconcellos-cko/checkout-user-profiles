using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Requests;
using UserProfiles.Common.Models.Responses;

namespace UserProfiles.Api.Services
{
    public interface IUserService
    {
        Task CreateAsync(CreateAccountRequest request);

        Task EditAsync(EditAccountRequest request);

        Task AssignClaimAsync(AssignClaimToUserRequest request);

        Task AssignRoleAsync(AssignRoleToUserRequest request);

        Task<User> GetByNameAsync(string name);

        Task<GetUserPermissionsResponse> GetDetailsByIdAsync(int id);

        Task<List<GetUserPermissionsResponse>> GetDetailsAsync();

        Task<List<Claim>> GetClaimsByUserNameAsync(string userName);

        Task<List<ClaimBase>> GetClaimsByIdAsync(int id);

        Task<IList<string>> GetRolesByIdAsync(int id);
    }
}
