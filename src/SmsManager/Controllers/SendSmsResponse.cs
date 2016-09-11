using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SmsManager.Models;

namespace SmsManager.Controllers
{
    public class SendSmsResponse
    {
        [JsonProperty(PropertyName = "state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SmsStatus SmsStatus { get; set; }
    }
}