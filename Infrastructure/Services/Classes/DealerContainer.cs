using Newtonsoft.Json;
using System.Collections.Generic;

namespace FinalProject.Infrastructure.Services.Classes
{
    public class DealerContainer
    {
        [JsonProperty("num_found")]
        public int Counter { get; set; }

        [JsonProperty("dealers")]
        public List<DealerInfo> DealerViewModels { get; set; }
    }


}
