using D2MP.Models.Enums;
using Newtonsoft.Json;

namespace D2MP.Models.Converters
{
    public class PlayerSlotFactionConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Faction);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var slot = int.Parse(reader.Value.ToString());

            return slot < 5 ? Faction.Radiant : Faction.Dire;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
