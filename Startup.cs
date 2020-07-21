using FinalProject.Infrastructure.BackgroundServices;
using FinalProject.Infrastructure.Configuration;
using FinalProject.Infrastructure.Services.Classes;
using FinalProject.Infrastructure.Services.Implementations;
using FinalProject.Infrastructure.Services.Interfaces;
using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using FinalProject.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinalProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRestApiClient, RestApiClient>();

            var section = Configuration.GetSection("RestApiConfig");
            services.Configure<RestApiConfig>(section);

            services.AddDbContext<CarMarketContext>(builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("Azure"))
                .UseLazyLoadingProxies());

            services.AddSingleton<IVariablesKeeper, VariablesKeeper>();

            services.AddTransient<ICommonActions<Car>, CarRepository>();
            services.AddTransient<ICommonActions<CarPhotoLink>, CarPhotoLinkRepository>();
            services.AddTransient<ICommonActions<Country>, CountryRepository>();
            services.AddTransient<ICommonActions<Dealer>, DealerRepository>();
            services.AddTransient<ICommonActions<CarHistory>, CarHistoryRepository>();

            services.AddScoped<IScopeService<DealerLoad>, ScopeUploadService>();

            services.AddHostedService<LoadBaseInfoService>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<CarMarketContext>();

            services.Configure<IdentityOptions>(option =>
           {
               option.Password.RequireDigit = false;
               option.Password.RequireLowercase = false;
               option.Password.RequiredLength = 3;
               option.Password.RequiredUniqueChars = 0;
               option.Password.RequireUppercase = false;
               option.User.RequireUniqueEmail = false;
           });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
