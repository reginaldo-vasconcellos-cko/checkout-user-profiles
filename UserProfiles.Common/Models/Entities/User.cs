using System.ComponentModel.DataAnnotations;

namespace UserProfiles.Common.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string GuidRef { get; set; }
    }
}
