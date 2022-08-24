using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Client.Configuration;
using Twitter.Service.Models.Report;
using Twitter.Service.Services.Interfaces;

namespace Twitter.HostedServices
{
    public class RetrieveTwittsHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<InsertDataToDbHostedService> _logger;
        private readonly IHashTagService _reportService;
        private readonly IMapper mapper;
        private readonly IOptions<TwitterRetriveSettings> _options;
        private Timer? _timer = null;
        public RetrieveTwittsHostedService(
            ILogger<InsertDataToDbHostedService> logger, IMapper mapper, IHashTagService reportService, IOptions<TwitterRetriveSettings> options)
        {
            this._logger = logger;
            _reportService = reportService;
            this.mapper = mapper;
            _options = options;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Processing retrived twitts into console.");
                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        private void DoWork(object? state)
        {
            try
            {
                var result = _reportService.GetHashTags(_options.Value.TopRetrieveCount);
                var data = this.mapper.Map<HashTagReport>(result.Result);
                var listTags = data.HashTags.Select(x => x.Tag);
                _logger.LogInformation("Twitter HashTags:{data}/ TotalTwitts: {count}", string.Join(",", listTags), data.TotalTwitts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Processing retrived twitts into console stopping.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}