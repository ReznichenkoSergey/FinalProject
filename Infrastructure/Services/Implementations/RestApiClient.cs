using FinalProject.Infrastructure.Configuration;
using FinalProject.Infrastructure.Services.Classes;
using FinalProject.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Services.Implementations
{
    public class RestApiClient : IRestApiClient
    {
        readonly IOptions<RestApiConfig> _options;
        readonly IVariablesKeeper _keeper;

        public RestApiClient(IOptions<RestApiConfig> options, IVariablesKeeper keeper)
        {
            _options = options;
            _keeper = keeper;
        }

        public async Task<List<CountryInfo>> GetCountryInfoByRegion(CountryRegions regions)
        {
            var client = new RestClient(_options.Value.MarketCheck.Path);
            var path = $"{_options.Value.CountryCheck.Path}/{CountryRegions.Europe.ToString().ToLower()}";
            var request = new RestRequest(path, Method.GET, DataFormat.Json);

            var result = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<List<CountryInfo>>(result.Content, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
        }

        /// <summary>
        /// Получение списк а авто в продаже
        /// </summary>
        /// <returns></returns>
        public async Task<List<CarInfo>> GetActiveCarAsync(int? dealerId)
        {
            var client = new RestClient(_options.Value.MarketCheck.Path);
            var request = new RestRequest(_options.Value.MarketCheck.GetAllCars, Method.GET, DataFormat.Json);
            request.AddQueryParameter("api_key", _options.Value.MarketCheck.ApiKey);
            request.AddQueryParameter("include_lease", "false");
            request.AddQueryParameter("year", "2020");
            request.AddQueryParameter("rows", "50");
            request.AddQueryParameter("country", "US");
            if(dealerId.HasValue)
            {
                request.AddQueryParameter("dealer_id", $"{dealerId.Value}");
            }

            var result = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<CarContainer>(result.Content, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            }).CarInfos;
        }

        public async Task<List<CarVinCodeHistory>> GetCarHistoryByVinCodeAsync(string vinCode)
        {
            var client = new RestClient(_options.Value.MarketCheck.Path);
            var request = new RestRequest(_options.Value.MarketCheck.GetInfoByVinCode+$"/{vinCode}", Method.GET, DataFormat.Json);
            request.AddQueryParameter("api_key", _options.Value.MarketCheck.ApiKey);

            var result = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<List<CarVinCodeHistory>>(result.Content, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
        }

        public async Task<List<DealerInfo>> GetAllCarDealersAsync(int startIndex, int rowsCount = 50)
        {
            var client = new RestClient(_options.Value.MarketCheck.Path);
            var request = new RestRequest(_options.Value.MarketCheck.GetCarDealers, Method.GET, DataFormat.Json);
            request.AddQueryParameter("api_key", _options.Value.MarketCheck.ApiKey);
            request.AddQueryParameter("rows", rowsCount <= 50 ? rowsCount.ToString() : "50");//start
            request.AddQueryParameter("start", startIndex.ToString());

            var result = await client.ExecuteAsync(request);
            var dealerContainer = JsonConvert.DeserializeObject<DealerContainer>(result.Content, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            _keeper.SetVariable("DealerContainerCounter", dealerContainer.Counter);
            return dealerContainer.DealerViewModels;
        }

        public async Task<CarInfo> GetCarInfoByVinCodeAsync(string vinCode)
        {
            var client = new RestClient(_options.Value.MarketCheck.Path);
            var request = new RestRequest(_options.Value.MarketCheck.GetAllCars, Method.GET, DataFormat.Json);
            request.AddQueryParameter("api_key", _options.Value.MarketCheck.ApiKey);
            request.AddQueryParameter("vin", $"{vinCode}");

            var result = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<CarContainer>(result.Content, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            }).CarInfos[0];
        }

    }
}
