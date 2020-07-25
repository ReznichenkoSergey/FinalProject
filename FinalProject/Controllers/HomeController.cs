using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        ICommonActions<Car> _carSource;
        public HomeController(ICommonActions<Car> carSource)
        {
            _carSource = carSource;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var cars = await _carSource.GetAllAsync();
            var result = cars
                .OrderByDescending(x => x.DateUpdateInfo)
                .Take(20)
                .ToList();
            return View(result);
        }

    }
}
