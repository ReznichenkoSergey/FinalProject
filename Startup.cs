using FinalProject.Infrastructure.BackgroundServices;
using FinalProject.Infrastructure.Configuration;
using FinalProject.Infrastructure.Services.Classes;
using FinalProject.Infrastructure.Services.Implementations;
using FinalProject.Infrastructure.Services.Interfaces;
using FinalProject.Models.CarMarket;
using FinalProject.Models.Classes;
using FinalProject.Models.Interfaces;
using FinalProject.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
            services.AddSingleton<WebSocketsHandler>();

            var section = Configuration.GetSection("RestApiConfig");
            services.Configure<RestApiConfig>(section);

            var sectionEMail = Configuration.GetSection("EmailConfig");
            services.Configure<EmailConfig>(sectionEMail);

            services.AddDbContext<CarMarketContext>(builder =>
                //builder.UseSqlServer(Configuration.GetConnectionString("Local"))
                builder.UseSqlServer(Configuration.GetConnectionString("Azure"))
                .UseLazyLoadingProxies());

            services.AddSingleton<IVariablesKeeper, VariablesKeeper>();
            services.AddSingleton<IMessageService, MessageService>();

            services.AddTransient<ICommonActions<Car>, CarRepository>();
            services.AddTransient<ICommonActions<CarPhotoLink>, CarPhotoLinkRepository>();
            services.AddTransient<ICommonActions<Country>, CountryRepository>();
            services.AddTransient<ICommonActions<Dealer>, DealerRepository>();
            services.AddTransient<ICommonActions<CarHistory>, CarHistoryRepository>();

            services.AddScoped<IScopeService<DealerLoad>, ScopeUploadService>();

            services.AddHostedService<LoadBaseInfoService>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<CarMarketContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
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

            WebSocketOptions socketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromDays(1)
            };
            app.UseWebSockets(socketOptions);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

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
