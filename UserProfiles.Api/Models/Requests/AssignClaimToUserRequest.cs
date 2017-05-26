using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Models
{
    public class AssignClaimToUserRequest
    {
        public string Username { get; set; }

        public ClaimBase Claim { get; set; }
    }
}
