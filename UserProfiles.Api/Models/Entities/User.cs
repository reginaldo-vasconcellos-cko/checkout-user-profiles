using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string GuidRef { get; set; }
    }
}
