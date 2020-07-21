using FinalProject.Infrastructure.Services.Interfaces;
using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    [Route("[controller]")]
    public class CarHistoryController : Controller
    {
        readonly ICommonActions<CarHistory> _source;

        public CarHistoryController(ICommonActions<CarHistory> source)
        {
            _source = source;
        }

        [Route("All")]
        public async Task<IActionResult> GetAllCarHistoriesAsync()
        {
            var result = await _source.GetAllAsync();
            return View(result);
        }
    }
}
