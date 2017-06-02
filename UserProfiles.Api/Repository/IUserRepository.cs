using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Responses;

namespace UserProfiles.Api.Repository
{
    public interface IUserRepository
    {
        void Add(User user);

        void Update(User user);

        Task<List<GetUserPermissionsResponse>> GetDetailsByIdAsync(int? id = null);

        User GetByRefId(string id);

        Task<User> GetByIdAsync(int id);
    }
}
