using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class CountryController : Controller
    {
        readonly ICommonActions<Country> _source;

        public CountryController(ICommonActions<Country> source)
        {
            _source = source;
        }

        public async Task<IActionResult> GetAllCountriesAsync()
        {
            var result = await _source.GetAllAsync();
            return View(result);
        }

    }
}
