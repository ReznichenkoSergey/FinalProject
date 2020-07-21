using FinalProject.Infrastructure.Services.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Services.Interfaces
{
    public interface IRestApiClient
    {
        Task<List<CountryInfo>> GetCountryInfoByRegion(CountryRegions regions);

        Task<List<DealerInfo>> GetAllCarDealersAsync(int startIndex = 0, int rowsCount = 50);

        Task<List<CarInfo>> GetActiveCarAsync(int? dealerId);

        Task<CarInfo> GetCarInfoByVinCodeAsync(string vinCode);

        Task<List<CarVinCodeHistory>> GetCarHistoryByVinCodeAsync(string vinCode);
    }
}
