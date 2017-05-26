using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models.Entities
{
    public class Business
    {
        [Key]
        public long Id { get; set; }

        public long MerchantId { get; set; }

        public string Name { get; set; }
    }
}
