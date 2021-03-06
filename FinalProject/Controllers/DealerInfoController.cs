﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerInfoController : ControllerBase
    {
        readonly ICommonActions<Dealer> _dealerInfo;

        public DealerInfoController(ICommonActions<Dealer> dealerInfo)
        {
            _dealerInfo = dealerInfo;
        }

        [HttpGet("Country/{name?}")]
        public async Task<IActionResult> GetDealersByCountryAsync(string name, [FromHeader] int rowCounter = 10)
        {
            var dealerList = await _dealerInfo.GetAllAsync();
            var infolist = new List<DealerInfoViewModel>();
            if (!string.IsNullOrEmpty(name))
            {
                infolist.AddRange(dealerList
                .Where(x => x.Country.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                .Take(rowCounter)
                .Select(x => new DealerInfoViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ContactPhone = x.ContactPhone,
                    Status = x.Status.ToString(),
                    Street = x.Street,
                    City = x.City,
                    CountryState = x.CountryState,
                    CountryName = x.Country.Name,
                    WebSite = x.WebSite,
                    Zip = x.Zip
                }));
            }
            else
            {
                infolist.AddRange(dealerList
                .Take(rowCounter)
                .Select(x => new DealerInfoViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ContactPhone = x.ContactPhone,
                    Status = x.Status.ToString(),
                    Street = x.Street,
                    City = x.City,
                    CountryState = x.CountryState,
                    CountryName = x.Country.Name,
                    WebSite = x.WebSite,
                    Zip = x.Zip
                }));
            }
            var result = JsonConvert.SerializeObject(infolist);
            return new ObjectResult(result);
        }

        [HttpGet("Model/{name?}")]
        public async Task<IActionResult> GetDealersByCarModelAsync([FromServices] ICommonActions<Car> carInfo, string name, [FromHeader] int rowCounter = 10)
        {
            var dealerList = await _dealerInfo.GetAllAsync();
            var infolist = new List<DealerInfoViewModel>();
            if (!string.IsNullOrEmpty(name))
            {
                var carList = await carInfo.GetAllAsync();
                var dealerIdList = carList
                    .Where(x => x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                    .Select(x => x.DealerId)
                    .Distinct();
                
               infolist.AddRange(dealerList
                    .Where(x => dealerIdList.Contains(x.Id))
                    .Take(rowCounter)
                    .Select(x => new DealerInfoViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ContactPhone = x.ContactPhone,
                        Status = x.Status.ToString(),
                        Street = x.Street,
                        City = x.City,
                        CountryState = x.CountryState,
                        CountryName = x.Country.Name,
                        WebSite = x.WebSite,
                        Zip = x.Zip
                    }));
            }
            else
            {
                infolist.AddRange(dealerList
                .Take(rowCounter)
                .Select(x => new DealerInfoViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ContactPhone = x.ContactPhone,
                    Status = x.Status.ToString(),
                    Street = x.Street,
                    City = x.City,
                    CountryState = x.CountryState,
                    CountryName = x.Country.Name,
                    WebSite = x.WebSite,
                    Zip = x.Zip
                }));
            }
            var result = JsonConvert.SerializeObject(infolist);
            return new ObjectResult(result);
        }

        [HttpPost("save")]
        public async Task AddAsync([FromServices] ICommonActions<Country> countrySource, [FromBody] DealerInfoViewModel model)
        {
            if (model == null)
                return;
            
            var countryList = await countrySource.GetAllAsync();
            var country = countryList
                .Where(x => x.Name.Equals(model.CountryName, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            
            if(country==null)
                return;

            Dealer dealer = new Dealer()
            {
                Id = model.Id,
                Name = model.Name,
                ContactPhone = model.ContactPhone,
                Status = model.Status.Equals("Active", StringComparison.InvariantCultureIgnoreCase) ? DealerStatus.Active : DealerStatus.Unknown,
                Street = model.Street,
                City = model.City,
                CountryState = model.CountryState,
                Country = country,
                WebSite = model.WebSite,
                Zip = model.Zip
            };
            await _dealerInfo.AddAsync(dealer);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _dealerInfo.DeleteAsync(id);
        }

    }
}
