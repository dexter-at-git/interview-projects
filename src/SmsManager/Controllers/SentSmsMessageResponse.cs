using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SmsManager.Models;

namespace SmsManager.Controllers
{
    public class SentSmsMessageResponse
    {
        [JsonProperty(PropertyName = "dateTime")]
        public DateTime DateSent { get; set; }

        [JsonProperty(PropertyName = "mcc")]
        public string MobileCountryCode { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string CountryName { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SmsStatus SmsStatus { get; set; }

    }
}