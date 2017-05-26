using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models.Entities
{
    public class UserResourceIdentity
    {
        public int UserId { get; set; }

        public int ResourceIdentityId { get; set; }
    }
}
