using Newtonsoft.Json;
using System;

namespace FinalProject.Infrastructure.Services.Classes
{
    public class CarVinCodeHistory
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("vdp_url")]
        public string PageUrl { get; set; }

        [JsonProperty("seller_type")]
        public string SellerType { get; set; }

        [JsonProperty("inventory_type")]
        public string CarState { get; set; }

        [JsonProperty("last_seen_at_date")]
        public DateTime DateLastSeen { get; set; }

        [JsonProperty("scraped_at_date")]
        public DateTime DateScraped { get; set; }

        [JsonProperty("first_seen_at_date")]
        public DateTime DateFirstSeen { get; set; }

        [JsonProperty("seller_name")]
        public string SellerName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string CountryState { get; set; }

        [JsonProperty("zip")]
        public string ZipCode { get; set; }
    }
}
