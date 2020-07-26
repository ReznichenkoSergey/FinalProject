using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    [Authorize]
    public class CarDealerController : Controller
    {
        readonly ICommonActions<Dealer> _source;

        public CarDealerController(ICommonActions<Dealer> source)
        {
            _source = source;
        }

        public async Task<IActionResult> GetAllDealersAsync(string name)
        {
            var result = await _source.GetAllAsync();
            if (!string.IsNullOrEmpty(name))
                return View(result
                    .Where(x => x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                    .ToList());
            else
                return View(result);
        }

        public async Task<IActionResult> GetAllDealersByCountryAsync(string countryCode)
        {
            var result = await _source.GetAllAsync();
            if (!string.IsNullOrEmpty(countryCode))
                return View("GetAllDealers", result.Where(x => x.Country.Code.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase)).ToList());
            else
                return View("GetAllDealers", result);
        }

    }
}
