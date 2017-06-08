using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Responses;

namespace UserProfiles.Data.Repository
{
    public interface IUserRepository
    {
        Task<int> Add(User user);

        void Update(User user);

        Task<List<GetUserPermissionsResponse>> GetDetailsByIdAsync(int? id = null);

        User GetByRefId(string id);

        Task<User> GetByIdAsync(int id);
    }
}
