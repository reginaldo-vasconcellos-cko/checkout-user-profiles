using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfiles.Api.Models.Requests;

namespace UserProfiles.Api.Repository
{
    public interface IUserResouceIdentityRepository
    {
        bool VerifyUserResouceIdentityPermission(VerifyUserResouceIdentityPermissionRequest request);

        void InsertUserResouceIdentity(int userId, int resourceId);
    }
}
