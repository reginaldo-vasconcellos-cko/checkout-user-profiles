using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Common.Models.Requests
{
    public class AssignClaimToUserRequest
    {
        public string Username { get; set; }

        public ClaimBase Claim { get; set; }
    }
}
