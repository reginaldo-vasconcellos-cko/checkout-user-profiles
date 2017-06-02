using System.Collections.Generic;

namespace UserProfiles.Api.Models.Requests
{
    public class EditAccountRequest
    {
        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public List<string> Claims { get; set; }
    }
}
