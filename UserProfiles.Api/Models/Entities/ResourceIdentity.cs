using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserProfiles.Api.Models.Enums;

namespace UserProfiles.Api.Models.Entities
{
    public class ResourceIdentity
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IdentityType Type { get; set; }

        public string Name { get; set; }
    }
}
