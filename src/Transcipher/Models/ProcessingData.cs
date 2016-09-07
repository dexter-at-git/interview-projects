using Newtonsoft.Json;

namespace Transcipher.Controllers
{
    public class ProcessingData
    {
        [JsonProperty(PropertyName = "algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}