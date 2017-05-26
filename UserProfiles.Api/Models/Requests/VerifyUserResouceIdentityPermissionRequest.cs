using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models.Requests
{
    public class VerifyUserResouceIdentityPermissionRequest
    {
        public int UserId { get; set; }

        public int IdentityType { get; set; }

        public int IdentityId { get; set; }
    }
}
