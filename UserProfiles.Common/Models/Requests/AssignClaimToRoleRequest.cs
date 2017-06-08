using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Common.Models.Requests
{
    public class AssignClaimToRoleRequest
    {
        public string Role { get; set; }

        public ClaimBase[] Claims { get; set; }
    }
}
