namespace UserProfiles.Common.Models.Requests
{
    public class VerifyUserResouceIdentityPermissionRequest
    {
        public int UserId { get; set; }

        public int IdentityType { get; set; }

        public int IdentityId { get; set; }
    }
}
