using System;
using System.Collections.Generic;
using System.Text;
using UserProfiles.Common.Models.Requests;

namespace UserProfiles.Api.Services
{
    public interface IUserResourceIdentityService
    {
        bool VerifyUserResouceIdentityPermission(VerifyUserResouceIdentityPermissionRequest request);

        void InsertUserResouceIdentity(int userId, int resourceId);
    }
}
