using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;
using Twitter.Client.Configuration;
using Twitter.DataAccess.Repositories.Interfaces;
using Twitter.DataAccess.Repositories;
using Twitter.HostedServices;
using System.Net.Http;
using Polly.Extensions.Http;
using Twitter.Client.Interfaces;
using Twitter.Client.Services;
using Polly;
using Twitter.Service.Services.Interfaces;
using Twitter.Service.Services;
using System.Reflection;
using Serilog.Events;

namespace Twitter
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            //SeriLog Config
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                 .MinimumLevel.Verbose()
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            //Registering services
            var host = Host.CreateDefaultBuilder()
                       .ConfigureServices((context, services) =>
                       {                          
                           // Configuration
                           services.Configure<ClientConfiguration>(options => context.Configuration.GetSection("ApiKeys").GetSection("Twitter").Bind(options));
                           services.Configure<TwitterRetriveSettings>(options => context.Configuration.GetSection("RetrieveSetting").GetSection("Twitter").Bind(options));

                           // HostedServices
                           services.AddHostedService<RetrieveDataFromExternalServiceHostedService>();
                           services.AddHostedService<InsertDataToDbHostedService>();
                           services.AddHostedService<RetrieveTwittsHostedService>();

                           // HttpClients
                           services.AddHttpClient<ITwiitAPI, TwiitAPI>()
                            .AddPolicyHandler(GetRetryPolicy());

                           // Automapper
                           services.AddAutoMapper(Assembly.GetExecutingAssembly(), (typeof(TwittService).Assembly));

                           // Services                           
                           services.AddSingleton<ITwitterQueueManager, ConcurrentQueueManager>();
                           services.AddSingleton<ITwittProcessingService, TwiitProcessService>();
                           services.AddSingleton<ITwitterConsumerService, TwitterRetrieveService>();
                           services.AddSingleton<ITwittService, TwittService>();
                           services.AddSingleton<IHashTagService, HashTagService>();

                           //Repositories
                           services.AddSingleton<ITwittRepository, TwittRepository>();
                           services.AddSingleton<IHashTagRepository, HashTagRepository>();
                       })
                       .UseSerilog()
                       .Build();


            await host.RunAsync();

        }
        /// <summary>
        /// BuildConfig
        /// </summary>
        /// <param name="builder"></param>
        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVRIONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }

        /// <summary>
        /// Poly Retry policy
        /// </summary>
        /// <returns></returns>
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var retryCount = 5;
            return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
               .WaitAndRetryAsync(
                    retryCount,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt))
                    );
        }
    }
}
