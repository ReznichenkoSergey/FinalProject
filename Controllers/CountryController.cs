using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    //[Route("[controller]")]
    public class CountryController : Controller
    {
        readonly ICommonActions<Country> _source;

        public CountryController(ICommonActions<Country> source)
        {
            _source = source;
        }

        //[Route("All")]
        public async Task<IActionResult> GetAllCountriesAsync()
        {
            var result = await _source.GetAllAsync();
            return View(result);
        }

        //[Route("Add")]
        /*[HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromForm] Country country)
        {
            if (ModelState.IsValid)
            {
                _source.AddAsync(country).Wait();
                return RedirectToAction("All");
            }
            return View();
        }

        //[Route("Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = _source.GetByIdAsync(id).Result;
            return View(obj);
        }

        //[Route("Delete")]
        [HttpDelete]
        public IActionResult Delete(int id, [FromForm] Country country)
        {
            _source.DeleteAsync(country.Id).Wait();

            return RedirectToAction("All");
        }


        //[Route("Modify/{id}")]
        [HttpGet]
        public IActionResult Modify(int id)
        {
            var obj = _source.GetByIdAsync(id).Result;
            return View(obj);
        }

        //[Route("Modify")]
        [HttpPut]
        public IActionResult Modify1([FromForm] Country country)
        {
            _source.ModifyAsync(country.Id, country).Wait();

            return RedirectToAction("All");
        }*/

    }
}
