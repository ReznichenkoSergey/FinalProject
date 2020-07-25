using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Configuration
{
    public class RestApiConfig
    {
        public MarketCheck MarketCheck { get; set; }
        public CountryCheck CountryCheck { get; set; }
    }

    public class MarketCheck
    {
        public string Path { get; set; }
        public string ApiKey { get; set; }
        public string GetAllCars { get; set; }
        public string GetInfoByVinCode { get; set; }
        public string GetCarDealers { get; set; }
        public string GetMotoDealers { get; set; }
    }

    public class CountryCheck
    {
        public string Path { get; set; }
    }
}
