using System.Collections.Generic;

namespace UserProfiles.Api.Models.Entities
{
    public class Role
    {
        public string Name { get; set; }

        public List<ClaimBase> Claims { get; set; }
    }
}
