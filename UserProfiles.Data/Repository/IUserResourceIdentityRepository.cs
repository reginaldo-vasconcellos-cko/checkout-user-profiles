using UserProfiles.Common.Models.Requests;

namespace UserProfiles.Data.Repository
{
    public interface IUserResourceIdentityRepository
    {
        bool VerifyUserResourceIdentityPermission(VerifyUserResouceIdentityPermissionRequest request);

        void InsertUserResourceIdentity(int userId, int resourceId);

        void ResetUserResources(int userId);
    }
}
