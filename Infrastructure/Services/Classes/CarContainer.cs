using Newtonsoft.Json;
using System.Collections.Generic;

namespace FinalProject.Infrastructure.Services.Classes
{
    public class CarContainer
    {
        [JsonProperty("num_found")]
        public int Count { get; set; }

        [JsonProperty("listings")]
        public List<CarInfo> CarInfos { get; set; }
    }

}
