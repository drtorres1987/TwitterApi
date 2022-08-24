using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Service.Services.Interfaces;

namespace Twitter.HostedServices
{
    public class InsertDataToDbHostedService : BackgroundService
    {
        private readonly ILogger<InsertDataToDbHostedService> _logger;
        private readonly ITwittProcessingService _twittProcessingService;

        public InsertDataToDbHostedService(
            ILogger<InsertDataToDbHostedService> logger,
            IServiceScopeFactory scopeFactory, ITwittProcessingService twittProcessingService)
        {
            this._logger = logger;
            _twittProcessingService = twittProcessingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Processing Twtitts Hosted Service running.");
                await DoWork(stoppingToken);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            await _twittProcessingService.ProcessTwittsAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Processing Twtitts Hosted Service is stopping.");
            await base.StopAsync(stoppingToken);
        }
    }
}