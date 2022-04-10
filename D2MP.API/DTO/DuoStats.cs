using System.Text.Json.Serialization;

namespace D2MP.API.DTO
{
    public class DuoStats
    {
        [JsonPropertyName("1")]
        public int Hero1Id { get; set; }
        [JsonPropertyName("2")]
        public int Hero2Id { get; set; }
        [JsonPropertyName("w")]
        public double WinRate { get; set; }
        [JsonPropertyName("m")]
        public int MatchCount { get; set; }
    }
}
