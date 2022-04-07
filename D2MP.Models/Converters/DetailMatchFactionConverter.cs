using D2MP.Models.Enums;
using Newtonsoft.Json;

namespace D2MP.Models.Converters
{
    public class DetailMatchFactionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Faction);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return bool.Parse(reader.Value.ToString()) ? Faction.Radiant : Faction.Dire;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
