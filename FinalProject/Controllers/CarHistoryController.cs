using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class CarHistoryController : Controller
    {
        readonly ICommonActions<CarHistory> _source;

        public CarHistoryController(ICommonActions<CarHistory> source)
        {
            _source = source;
        }

        public async Task<IActionResult> GetHistoryByVinAsync([FromQuery] string vin)
        {
            var vinList = await _source.GetAllAsync();
            var result = vinList
                .Where(x => x.Car.VinCode.Equals(vin, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
            return View(result);
        }
    }
}
