using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Responses;

namespace UserProfiles.Api.Repository
{
    public interface IUserRepository
    {
        void Add(User user);

        void Update(User user);

        GetUserPermissionsResponse GetById(int id);

        User GetByRefId(string id);
    }
}
