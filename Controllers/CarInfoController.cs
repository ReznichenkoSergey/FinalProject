using System;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarInfoController : ControllerBase
    {

        readonly ICommonActions<Car> _carinfo;

        public CarInfoController(ICommonActions<Car> carinfo)
        {
            _carinfo = carinfo;
        }

        [HttpGet("Model/{name?}")]
        public async Task<IActionResult> GetCarInfoAsync(string name, [FromHeader] int rowCounter = 10)
        {
            var carList = await _carinfo.GetAllAsync();
            var infolist = carList
                .Where(x => x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                .Take(rowCounter)
                .Select(x => new CarInfoViewModel()
                {
                    Id = x.Id,
                    CarState = x.CarState.ToString(),
                    ColorExterior = x.ColorExterior,
                    ColorInterior = x.ColorInterior,
                    DealerId = x.DealerId,
                    DealerName = x.Dealer.Name,
                    Name = x.Name,
                    Price = x.Price,
                    VinCode = x.VinCode
                }).ToList();
            var result = JsonConvert.SerializeObject(infolist);
            return new ObjectResult(result);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetCarInfoAsync(int id, [FromHeader] int rowCounter = 10)
        {
            var carList = await _carinfo.GetAllAsync();
            var infolist = carList
                .Where(x => x.Id == id)
                .Take(rowCounter)
                .Select(x => new CarInfoViewModel()
                {
                    Id = x.Id,
                    CarState = x.CarState.ToString(),
                    ColorExterior = x.ColorExterior,
                    ColorInterior = x.ColorInterior,
                    DealerId = x.DealerId,
                    DealerName = x.Dealer.Name,
                    Name = x.Name,
                    Price = x.Price,
                    VinCode = x.VinCode
                }).ToList();
            var result = JsonConvert.SerializeObject(infolist);
            return new ObjectResult(result);
        }

    }
}
