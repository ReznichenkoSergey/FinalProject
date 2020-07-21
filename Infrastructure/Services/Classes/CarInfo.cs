using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace FinalProject.Infrastructure.Services.Classes
{
    public class CarInfo
    {
        public string Id { get; set; }

        [JsonProperty("vin")]
        public string VinCode { get; set; }

        [JsonProperty("heading")]
        public string ModelName { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("vdp_url")]
        public string UrlPage { get; set; }

        [JsonProperty("exterior_color")]
        public string ColorExterior { get; set; }

        [JsonProperty("interior_color")]
        public string ColorInterior { get; set; }

        [JsonProperty("last_seen_at_date")]
        public DateTime DateUpdateInfo { get; set; }

        [JsonProperty("seller_type")]
        public string SellerType { get; set; }

        [JsonProperty("inventory_type")]
        public string CarState { get; set; }

        [JsonProperty("availability_status")]
        public string CarStatus { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }
    }

    public class Media
    {
        [JsonProperty("photo_links")]
        public List<string> Photos { get; set; }
    }
}