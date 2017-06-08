namespace UserProfiles.Common.Models.Requests
{
    public class AssignResourceToUserRequest
    {
        public int UserId { get; set; }

        public int[] ResourceIdentityId { get; set; }
    }
}
