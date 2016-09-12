using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmsManager.Data;
using SmsManager.Models.Profiles;
using SmsManager.Repositories;
using SmsManager.Repositories.Interfaces;
using SmsManager.Services;
using SmsManager.Services.Interfaces;
using Transcipher.Algorithms;
using Transcipher.Services;

namespace InterviewProjects.Shell
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITranscipherService, TranscipherService>();
            services.AddTransient<IAlgorithmFactory, AlgorithmFactory>();


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
            {
                config.RespectBrowserAcceptHeader = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter());
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });


            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
