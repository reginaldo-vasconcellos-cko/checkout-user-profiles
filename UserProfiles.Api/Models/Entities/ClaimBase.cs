using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models.Entities
{
    public class ClaimBase
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }
}
