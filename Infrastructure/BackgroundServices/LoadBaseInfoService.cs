using FinalProject.Infrastructure.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.BackgroundServices
{
    public class LoadBaseInfoService : BackgroundService
    {
        /*readonly IRestApiClient _client;
        readonly IVariablesKeeper _keeper;
        readonly ICommonActions<Dealer> _dbDealer;
        readonly ICommonActions<Country> _dbCountry;

        public LoadMarketInfoService(IRestApiClient client,
            IVariablesKeeper keeper,
            ICommonActions<Dealer> dbDealer,
            ICommonActions<Country> dbCountry)
        {
            _client = client;
            _keeper = keeper;
            _dbDealer = dbDealer;
            _dbCountry = dbCountry;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
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
            }
        }

        private async Task SetDealerAsync(List<DealerInfo> list)
        {
            var dealers = list.Select(x =>
            new Dealer()
            {
                Id = x.Id,
                Name = x.SellerName,
                Status = DealerStatus.Active, //!!!!!!!!!!!!
                Street = x.Street,
                City = x.City,
                CountryState = x.CountryState,
                Zip = x.Zip,
                ContactPhone = x.ContactPhone,
                CountryId = _dbCountry.GetByName(x.Country).Result.Id
            }).ToList();
            await _dbDealer.AddRangeAsync(dealers);
        }*/

        readonly IServiceProvider _services;
        public LoadBaseInfoService(IServiceProvider services)
        {
            _services = services;
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            await scope
                .ServiceProvider
                .GetRequiredService<IScopeService<DealerLoad>>()
                .DoWorkAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

    }
}
