using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmsManager.Data;
using Microsoft.EntityFrameworkCore;
using SmsManager.Models;
using SmsManager.Repositories;
using SmsManager.Services;
using Serilog;
using Microsoft.AspNet.Routing.Template;
using SmsManager.Data.Entities;

namespace SmsManager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RollingFile(Path.Combine(env.WebRootPath, "log-{Date}.txt"))
                .CreateLogger();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["Production:SqliteConnectionString"];

            services.AddDbContext<SmsManagerContext>(options =>
                options.UseSqlite(connection)
            );

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SmsManagerProfile());
            });

            services.AddSingleton<IMapper>(sp => mapperConfig.CreateMapper());

            services.AddTransient<ISmsManagerService, SmsManagerService>();

            services.AddTransient<ISmsMessageRepository, SmsMessageRepository>();
            services.AddTransient<ICountryRepository, CountryRepoisitory>();
            services.AddTransient<ISmsSender, SmsSender>();

            services.AddMvc(config =>
            {/*      
                // Add XML Content Negotiation
                config.RespectBrowserAcceptHeader = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter());
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
         
                 */

            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                // route1
                routes.MapRoute(
                   name: "default",
                   template: "{controller}/{action}.{extension}"
                //   defaults: new { controller = "Products", action = "Index" }
                );
            });

            /*
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "text/xml");
            */
        }
    }
    


    public class SmsManagerProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SmsMessageEntity, SmsMessage>();
            CreateMap<CountryEntity, Country>();


            CreateMap<SmsMessage, SmsMessageEntity>();
            CreateMap<Country, CountryEntity>();
        }
    }
}
