using System.ComponentModel.DataAnnotations;

namespace UserProfiles.Common.Models.Entities
{
    public class Merchant
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
