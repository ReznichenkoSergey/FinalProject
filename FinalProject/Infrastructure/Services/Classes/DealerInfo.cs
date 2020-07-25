using Newtonsoft.Json;

namespace FinalProject.Infrastructure.Services.Classes
{
    public class DealerInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("seller_name")]
        public string SellerName { get; set; }

        [JsonProperty("inventory_url")]
        public string InventoryUrl { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string CountryState { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("seller_phone")]
        public string ContactPhone { get; set; }
    }
}
