using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserProfiles.Common.Models.Enums;

namespace UserProfiles.Common.Models.Entities
{
    public class ResourceIdentityDto
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IdentityType Type { get; set; }

        public string Name { get; set; }
    }
}
