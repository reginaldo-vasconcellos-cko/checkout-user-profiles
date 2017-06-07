using System.Collections.Generic;
using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Common.Models.Responses
{
    public class GetUserPermissionsResponse
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public List<RoleDto> Roles { get; set; }

        public List<ClaimBase> Claims { get; set; }

        public List<ResourceIdentityDto> ResourceAccesses { get; set; }
    }
}
