using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models.Entities
{
    public class RoleBase
    {
        public string Name { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
