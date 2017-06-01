using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models
{
    public class AssignRoleToUserRequest
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
