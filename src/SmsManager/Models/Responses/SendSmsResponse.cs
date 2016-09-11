using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SmsManager.Models.Responses
{
    public class SendSmsResponse
    {
        [JsonProperty(PropertyName = "state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SmsStatus SmsStatus { get; set; }
    }
}