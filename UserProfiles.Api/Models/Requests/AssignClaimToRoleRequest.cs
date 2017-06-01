using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Models
{
    public class AssignClaimToRoleRequest
    {
        public string Role { get; set; }

        public ClaimBase[] Claims { get; set; }
    }
}
