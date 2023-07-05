using Newtonsoft.Json;
namespace TrustyBot.Modules.AdviceCommand
{
    public class AdviceData
    {
        [JsonProperty("slip")]
        public Slip Slip { get; set; }
    }

    public class Slip
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("advice")]
        public string Advice { get; set; }
    }

}
