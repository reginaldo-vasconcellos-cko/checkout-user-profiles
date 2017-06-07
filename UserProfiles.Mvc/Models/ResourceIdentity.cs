using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserProfiles.Common.Models.Enums;

namespace UserProfiles.Mvc.Models
{
    public class ResourceIdentity
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IdentityType Type { get; set; }

        public string Name { get; set; }
    }
}
