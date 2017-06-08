using System;
using System.Collections.Generic;
using System.Text;
using UserProfiles.Common.Models.Requests;
using UserProfiles.Data.Repository;

namespace UserProfiles.Api.Services
{
    public class UserResourceIdentityService : IUserResourceIdentityService
    {
        private readonly IUserResourceIdentityRepository _userResouceIdentityRepository;

        public UserResourceIdentityService(IUserResourceIdentityRepository userResouceIdentityRepository)
        {
            _userResouceIdentityRepository = userResouceIdentityRepository;
        }

        public bool VerifyUserResouceIdentityPermission(VerifyUserResouceIdentityPermissionRequest request)
        {
            return _userResouceIdentityRepository.VerifyUserResourceIdentityPermission(request);
        }

        public void InsertUserResouceIdentity(int userId, int resourceId)
        {
            _userResouceIdentityRepository.InsertUserResourceIdentity(userId, resourceId);
        }
    }
}
