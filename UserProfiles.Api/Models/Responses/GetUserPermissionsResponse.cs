using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Models.Responses
{
    public class GetUserPermissionsResponse
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public List<Role> Roles { get; set; }

        public List<ClaimBase> Claims { get; set; }

        //public string RoleName { get; set; }

        //public string RoleClaimType { get; set; }

        //public string RoleClaimValue { get; set; }

        //public string UserClaimType { get; set; }

        //public string UserClaimValue { get; set; }
    }
}
