using System.Text.Json.Serialization;

namespace TestApplication.Models
{
    public class NavHistoryModel
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        [JsonPropertyName("navHistory")]
        public Navhistory NavHistory { get; set; }
    }

    public class Navhistory
    {
        [JsonPropertyName("navHistory")]
        public NavhistoryData[] NavHistory { get; set; }
    }

    public class NavhistoryData
    {
        [JsonPropertyName("countryOfQuotation")]
        public string CountryOfQuotation { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("nav")]
        public string Nav { get; set; }
        [JsonPropertyName("navDate")]
        public string NavDate { get; set; }
        [JsonPropertyName("receivedTime")]
        public string ReceivedTime { get; set; }
        [JsonPropertyName("sourcePriority")]
        public string SourcePriority { get; set; }
        [JsonPropertyName("volumeRank")]
        public string VolumeRank { get; set; }
    }

}
