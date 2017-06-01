using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Api.Models;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Models.Responses;

namespace UserProfiles.Api.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterAccountRequest request);

        Task<List<Claim>> GetClaimsByUserNameAsync(string userName);

        Task AssignClaimAsync(AssignClaimToUserRequest request);

        Task AssignRoleAsync(AssignRoleToUserRequest request);

        void AssignResource(AssignResourceToUserRequest request);

        Task<User> GetByNameAsync(string name);

        Task<GetUserPermissionsResponse> GetByIdAsync(int id);
    }
}
