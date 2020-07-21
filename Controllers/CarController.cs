using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    //[Route("[controller]")]
    public class CarController : Controller
    {
        readonly ICommonActions<Car> _source;

        public CarController(ICommonActions<Car> source)
        {
            _source = source;
        }

        //[Route("All")]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var result = await _source.GetAllAsync();
            return View(result);
        }

        public async Task<IActionResult> GetCarsByDealerIdAsync(int? dealerId)
        {
            if (!dealerId.HasValue)
                return new BadRequestResult();

            var task = await _source.GetAllAsync();
            var result = task.Where(x => x.Dealer.Id == dealerId).ToList();
            ViewData["CarDealer"] = result.Count() > 0 ? result[0].Dealer : null;
            return View(result);
        }

        public async Task<IActionResult> GetSingleCarAsync(string vinCode)
        {
            if (string.IsNullOrEmpty(vinCode))
                return new BadRequestResult();

            var task = await _source.GetAllAsync();
            var result = task.Where(x => x.VinCode.Equals(vinCode, System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(result);
        }

        //public async Task<IActionResult> GetActiveCarsAsync()
        //{
        //    //var result = await _marketCheckQuery.GetActiveCarAsync(null);
        //    return View(null);
        //}

        //public async Task<IActionResult> GetCarsByDealerIdAsync(int? dealerId)
        //{
        //    if (!dealerId.HasValue)
        //        return new BadRequestResult();

        //    var result = await _marketCheckQuery.GetActiveCarAsync(dealerId);
        //    return View(result);
        //}

        //public async Task<IActionResult> GetSingleCarAsync(string vinCode)
        //{
        //    if (string.IsNullOrEmpty(vinCode))
        //        return new BadRequestResult();

        //    var result = await _marketCheckQuery.GetCarInfoByVinCodeAsync(vinCode);
        //    return View(result);
        //}

    }
}
