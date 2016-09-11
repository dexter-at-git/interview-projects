using Newtonsoft.Json;

namespace SmsManager.Controllers
{
    public class StatisticsResponse
    {
        [JsonProperty(PropertyName = "day")]
        public string Day { get; set; }

        [JsonProperty(PropertyName = "mcc")]
        public string MobileCountryCode { get; set; }

        [JsonProperty(PropertyName = "pricePerSMS")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "totalPrice")]
        public decimal TotalPrice { get; set; }
    }
}