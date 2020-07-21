using FinalProject.Infrastructure.Services.Implementations;
using FinalProject.Infrastructure.Services.Interfaces;
using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Services.Classes
{
    public class ScopeUploadService : IScopeService<DealerLoad>
    {
        readonly IRestApiClient _client;
        readonly IVariablesKeeper _keeper;
        readonly ICommonActions<Dealer> _dbDealer;
        readonly ICommonActions<Country> _dbCountry;
        readonly ICommonActions<Car> _dbCar;
        readonly ICommonActions<CarPhotoLink> _dbCarPhotoLink;

        public ScopeUploadService(IRestApiClient client,
            IVariablesKeeper keeper,
            ICommonActions<Dealer> dbDealer,
            ICommonActions<Country> dbCountry,
            ICommonActions<Car> dbCar,
            ICommonActions<CarPhotoLink> dbCarPhotoLink)
        {
            _client = client;
            _keeper = keeper;
            _dbDealer = dbDealer;
            _dbCountry = dbCountry;
            _dbCar = dbCar;
            _dbCarPhotoLink = dbCarPhotoLink;
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Default countries
                var result = await _dbCountry.GetAllAsync();
                if (!result.Any())
                {
                    var countriesInfo = await _client.GetCountryInfoByRegion(CountryRegions.Europe);
                    var countries = countriesInfo
                        .Where(x => !string.IsNullOrEmpty(x.Code) && string.IsNullOrEmpty(x.Name))
                        .Select(x => new Country()
                        {
                            Code = x.Code,
                            Name = x.Name
                        }).ToList();
                    countriesInfo = await _client.GetCountryInfoByRegion(CountryRegions.Americas);
                    countries.AddRange(countriesInfo
                        .Select(x => new Country()
                        {
                            Code = x.Code,
                            Name = x.Name
                        }));

                    await _dbCountry.AddRangeAsync(countries);
                }
                
                //Dealers
                var dealers = await _dbDealer.GetAllAsync();
                if(!dealers.Any())
                {
                    var dealersInfo = await _client.GetAllCarDealersAsync();
                    await SetDealerAsync(dealersInfo.ToList());

                    int rowCount = dealers.Count;
                    var dealersAmount = Convert.ToInt32(_keeper.GetVariableByKey("DealerContainerCounter"));

                    var circlesValues = Convert.ToInt32(Math.Round(dealersAmount / (decimal)rowCount, MidpointRounding.AwayFromZero));
                    //for (int i = 1; i < circlesValues; i++)
                    for (int i = 1; i < 2; i++)
                    {
                        dealersInfo = await _client.GetAllCarDealersAsync(rowCount * i + 1);

                        await SetDealerAsync(dealersInfo.ToList());
                    }
                }

                //Cars
                var cars = await _dbCar.GetAllAsync();
                if (!cars.Any())
                {
                    foreach (var dealer in dealers)
                    {
                        var carsInfoList = await _client.GetActiveCarAsync(dealer.NativeId);
                        await SetCarAsync(dealer, carsInfoList.ToList());
                    }
                }

                await Task.Delay(TimeSpan.FromDays(1));
            }
                /*while (!stoppingToken.IsCancellationRequested)
                {
                    var dealers = await _client.GetAllCarDealersAsync();
                    if (dealers != null)
                    {
                        await SetDealerAsync(dealers.ToList());

                        int rowCount = dealers.Count;
                        var dealersAmount = Convert.ToInt32(_keeper.GetVariableByKey("DealerContainerCounter"));

                        var circlesValues = Convert.ToInt32(Math.Round((decimal)dealersAmount / (decimal)rowCount, MidpointRounding.AwayFromZero));
                        for (int i = 1; i < circlesValues; i++)
                        {
                            dealers = await _client.GetAllCarDealersAsync(rowCount * i + 1);

                            await SetDealerAsync(dealers.ToList());
                        }
                        await Task.Delay(TimeSpan.FromDays(1)); //One time per day
                    }
                    else
                        await Task.Delay(TimeSpan.FromHours(1)); //One time per hour ... if failed
                }*/
            }

        private async Task SetDealerAsync(List<DealerInfo> list)
        {
            Dictionary<string, Country> countries = new Dictionary<string, Country>();
            list
                .Select(x => x.Country)
                .Distinct()
                .ToList()
                .ForEach(x =>
                {
                    Country country = _dbCountry.GetAllAsync().Result
                        .Where(c => c.Code.Equals(x, StringComparison.InvariantCultureIgnoreCase))
                        .FirstOrDefault();
                    countries.Add(x, country);
                }
                );
            foreach (var item in list)
            {
                Dealer dealer = new Dealer()
                {
                    NativeId = item.Id,
                    Name = item.SellerName,
                    Status = GetDealerStates(item.Status.ToLower()),
                    Street = item.Street,
                    City = item.City,
                    CountryState = item.CountryState,
                    Zip = item.Zip,
                    ContactPhone = item.ContactPhone,
                    Country = countries[item.Country]
                };
                await _dbDealer.AddAsync(dealer);
                //country.Dealers.Add(dealer);
                /*foreach (var item in list)
                {
                    Country country = _dbCountry.GetAllAsync().Result
                        .Where(c => c.Code.Equals(item.Country, StringComparison.InvariantCultureIgnoreCase))
                        .FirstOrDefault();
                    Dealer dealer = new Dealer()
                    {
                        NativeId = item.Id,
                        Name = item.SellerName,
                        Status = DealerStatus.Active, //!!!!!!!!!!!!
                        Street = item.Street,
                        City = item.City,
                        CountryState = item.CountryState,
                        Zip = item.Zip,
                        ContactPhone = item.ContactPhone,
                        Country = country
                    };
                    //country.Dealers.Add(dealer);
                    await _dbDealer.AddAsync(dealer);
                    //await _dbCountry.ModifyAsync(country.Id, country);
                }*/
                /*Dictionary<string, Country> countries = new Dictionary<string, Country>();
                list.Select(x => x.Country).Distinct().ToList().ForEach(x =>
                {
                    Country country = _dbCountry.GetAllAsync().Result
                        .Where(c => c.Code.Equals(x, StringComparison.InvariantCultureIgnoreCase))
                        .FirstOrDefault();
                    countries.Add(x, country);
                }
                    );

                var dealers = list
                    .Select(x =>new Dealer()
                    {
                        Id = x.Id,
                        Name = x.SellerName,
                        Status = DealerStatus.Active, //!!!!!!!!!!!!
                        Street = x.Street,
                        City = x.City,
                        CountryState = x.CountryState,
                        Zip = x.Zip,
                        ContactPhone = x.ContactPhone,
                        Country = _dbCountry
                                        .GetAllAsync()
                                        .Result
                                        .Where(c=> c.Code.Equals(x.Country, StringComparison.InvariantCultureIgnoreCase))
                                        .FirstOrDefault()
                        //CountryId = _dbCountry.GetByName(x.Country).Result.Id
                    }).ToList();
                await _dbDealer.AddRangeAsync(dealers);*/
            }
        }

        private async Task SetCarAsync(Dealer dealer, List<CarInfo> list)
        {
            foreach (var item in list)
            {
                Car car = new Car()
                {
                    NativeId = item.Id,
                    VinCode = item.VinCode,
                    Name = item.ModelName,
                    Price = item.Price,
                    UrlPage = item.UrlPage,
                    ColorExterior = item.ColorExterior,
                    ColorInterior = item.ColorInterior,
                    DateUpdateInfo = item.DateUpdateInfo,
                    CarState = item.CarState.Equals("new", StringComparison.InvariantCultureIgnoreCase)? CarState.New  : CarState.IsStock,
                    CarStatus = CarStatus.Active,
                    Dealer = dealer
                };
                await _dbCar.AddAsync(car);

                var carEntity = _dbCar.GetAllAsync().Result.Where(x=>x.NativeId.Equals(item.Id, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();
                foreach(var link in item.Media.Photos)
                {
                    CarPhotoLink carPhoto = new CarPhotoLink()
                    {
                        Url = link,
                        Car = carEntity
                    };
                    await _dbCarPhotoLink.AddAsync(carPhoto);
                }
            };
            /*public int Id { get; set; }

    [MaxLength(50)]
    public string DealerCode { get; set; }

    [MaxLength(30)]
    public string VinCode { get; set; }

    [Required, 
        MaxLength(50)]
    public string Name { get; set; }

    [Column(TypeName = "smallmoney"), 
        Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [MaxLength(50)]
    public string UrlPage { get; set; }

    [MaxLength(20)]
    public string ColorExterior { get; set; }

    [MaxLength(20)]
    public string ColorInterior { get; set; }

    public DateTime DateUpdateInfo { get; set; }

    public CarState CarState { get; set; } = CarState.New;

    public CarStatus CarStatus { get; set; } = CarStatus.Active;

    public int DealerId { get; set; }
    public virtual Dealer Dealer { get; set; }

    public virtual List<CarHistory> CarHistories { get; set; }*/
        }

        private DealerStatus GetDealerStates(string status) => status switch {
            "active" => DealerStatus.Active,
            _ => DealerStatus.Unknown
        };


    }
}
