using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfiles.Api.Models.Enums;

namespace UserProfiles.Api.Models.Requests
{
    public class AssignResourceToUserRequest
    {
        public int UserId { get; set; }

        public int[] ResourceIdentityId { get; set; }
    }
}
