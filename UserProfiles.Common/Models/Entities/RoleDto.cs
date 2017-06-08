using System.Collections.Generic;

namespace UserProfiles.Common.Models.Entities
{
    public class RoleDto
    {
        public string Name { get; set; }

        public List<ClaimBase> Claims { get; set; }
    }
}
