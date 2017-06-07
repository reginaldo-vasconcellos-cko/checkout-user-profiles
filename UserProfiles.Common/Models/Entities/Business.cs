using System.ComponentModel.DataAnnotations;

namespace UserProfiles.Common.Models.Entities
{
    public class Business
    {
        [Key]
        public long Id { get; set; }

        public long MerchantId { get; set; }

        public string Name { get; set; }
    }
}
