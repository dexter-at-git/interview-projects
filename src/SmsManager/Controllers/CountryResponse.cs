using Newtonsoft.Json;

namespace SmsManager.Controllers
{
    public class CountryResponse
    {
        [JsonProperty(PropertyName = "mcc")]
        public string MobileCountryCode { get; set; }

        [JsonProperty(PropertyName = "cc")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "pricePerSMS")]
        public decimal Price { get; set; }

    }
}