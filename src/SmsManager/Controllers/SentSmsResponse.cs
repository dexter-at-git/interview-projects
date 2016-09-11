using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmsManager.Controllers
{
    public class SentSmsResponse
    {
        [JsonProperty(PropertyName = "totalCount")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "items")]
        public IEnumerable<SentSmsMessageResponse> SmsMessageList { get; set; }
    }
}