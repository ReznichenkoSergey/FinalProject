using FinalProject.Infrastructure.Services.Interfaces;
using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class CarDealerController : Controller
    {
        readonly ICommonActions<Dealer> _source;

        public CarDealerController(ICommonActions<Dealer> source)
        {
            _source = source;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDealersAsync()
        {
            var result = await _source.GetAllAsync();
            return View(result);
        }

        [HttpGet]
        public IActionResult Add(int countryId)
        {
            return View(countryId);
        }

    }
}
