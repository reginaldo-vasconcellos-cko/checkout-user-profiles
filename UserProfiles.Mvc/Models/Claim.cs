using System.ComponentModel.DataAnnotations;

namespace UserProfiles.Mvc.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
