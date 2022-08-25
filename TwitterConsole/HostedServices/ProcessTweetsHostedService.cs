using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Service.Services.Interfaces;

namespace Twitter.HostedServices
{
    public class ProcessTweetsHostedService : BackgroundService
    {
        private readonly ILogger<ProcessTweetsHostedService> _logger;
        private readonly ITweetProcessingService _tweetProcessingService;

        public ProcessTweetsHostedService(
            ILogger<ProcessTweetsHostedService> logger,
            IServiceScopeFactory scopeFactory, ITweetProcessingService tweetProcessingService)
        {
            this._logger = logger;
            _tweetProcessingService = tweetProcessingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Processing Tweets Hosted Service is running.");
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
            await _tweetProcessingService.ProcessTweetsAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Processing Tweets Hosted Service is stopping.");
            await base.StopAsync(stoppingToken);
        }
    }
}