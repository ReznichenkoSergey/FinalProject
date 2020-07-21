using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FinalProject.Infrastructure.Services.Classes
{
    public class CountryInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alpha2Code")]
        public string Code { get; set; }
    }

    public enum CountryRegions
    {
        Africa, 
        Americas, 
        Asia, 
        Europe, 
        Oceania
    }
}