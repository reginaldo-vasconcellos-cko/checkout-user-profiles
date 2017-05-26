using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models
{
    public class AssignRoleToUserRequest
    {
        public string Username { get; set; }

        public string Role { get; set; }
    }
}
