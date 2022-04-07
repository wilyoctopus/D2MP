using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace D2MP.Models
{
    public class Hero
    {
        [JsonProperty("name")]
        [Column(TypeName = "name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        [Column(TypeName = "id")]
        public short Id { get; set; }

        [JsonProperty("localized_name")]
        [Column(TypeName = "localized_name")]
        public string LocalizedName { get; set; }
    }
}
