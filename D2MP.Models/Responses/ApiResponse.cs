using Newtonsoft.Json;

namespace D2MP.Models.Responses
{
    public class ApiResponse<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("response")]
        public T Response { get; set; }
    }
}
