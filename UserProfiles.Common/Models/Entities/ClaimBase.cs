using Newtonsoft.Json;

namespace UserProfiles.Common.Models.Entities
{
    public class ClaimBase
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}
