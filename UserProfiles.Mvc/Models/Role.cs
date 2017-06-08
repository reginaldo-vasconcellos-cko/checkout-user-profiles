using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserProfiles.Mvc.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }

        public List<Claim> Claims { get; set; }

        public string[] SelectedClaims { get; set; }
    }
}
