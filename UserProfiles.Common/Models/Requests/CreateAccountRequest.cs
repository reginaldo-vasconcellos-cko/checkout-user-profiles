using System.Collections.Generic;

namespace UserProfiles.Common.Models.Requests
{
    public class CreateAccountRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public List<string> Claims { get; set; }

        public int[] Resources { get; set; }
    }
}
