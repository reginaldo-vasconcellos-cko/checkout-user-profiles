using System.Threading.Tasks;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Responses;

namespace UserProfiles.Api.Repository
{
    public interface IUserRepository
    {
        void Add(User user);

        void Update(User user);

        Task<GetUserPermissionsResponse> GetByIdAsync(int id);

        User GetByRefId(string id);
    }
}
