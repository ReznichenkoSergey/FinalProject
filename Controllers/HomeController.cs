using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> IndexAsync([FromServices] ICommonActions<Car> carSource)
        {
            var cars = await carSource.GetAllAsync();
            var result = cars
                .OrderByDescending(x => x.DateUpdateInfo)
                .Take(20)
                .ToList();
            return View(result);
        }
    }
}
