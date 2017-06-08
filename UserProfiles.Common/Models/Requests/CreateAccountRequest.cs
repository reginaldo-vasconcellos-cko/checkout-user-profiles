using System.Collections.Generic;

namespace UserProfiles.Common.Models.Requests
{
    public class CreateAccountRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string[] Roles { get; set; }

        public string[] Claims { get; set; }

        public int[] Resources { get; set; }
    }
}
