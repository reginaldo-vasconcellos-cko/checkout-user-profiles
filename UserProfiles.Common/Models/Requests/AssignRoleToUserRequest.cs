namespace UserProfiles.Common.Models.Requests
{
    public class AssignRoleToUserRequest
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
