using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserProfiles.Mvc.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Invalid name, it can only contain letters and number")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<Claim> Claims { get; set; }

        public string[] SelectedClaims { get; set; }

        public List<Role> Roles { get; set; }

        public string[] SelectedRoles { get; set; }

        public List<ResourceIdentity> Resources { get; set; }

        public int[] SelectedResources { get; set; }
    }
}
