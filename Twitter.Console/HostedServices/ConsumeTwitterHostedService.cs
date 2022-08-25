using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Service.Services.Interfaces;

namespace Twitter.HostedServices
{
    public class ConsumeTwitterHostedService : BackgroundService
    {
        private readonly ILogger<ConsumeTwitterHostedService> _logger;
        private readonly ITwitterConsumerService _twitterConsumerService;
        

        public ConsumeTwitterHostedService(ILogger<ConsumeTwitterHostedService> logger,ITwitterConsumerService twitterConsumerService)
        {
            this._logger = logger;
            _twitterConsumerService = twitterConsumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {_logger.LogInformation("Queue the retrieved data into memory");

            await DoWork(stoppingToken);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            await _twitterConsumerService.ConsumeAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queue the retrieved data into memory stopping");

            await base.StopAsync(stoppingToken);
        }
    }
}