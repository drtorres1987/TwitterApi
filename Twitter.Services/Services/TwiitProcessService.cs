using Twitter.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Service.Services
{
    public class TwiitProcessService : ITwittProcessingService
    {
        private readonly ITwitterQueueManager _queue;
        private readonly ITwittService _service;
        private readonly ILogger _logger;

        public TwiitProcessService(ILogger<TwiitProcessService> logger, ITwitterQueueManager queue, ITwittService twittService)
        {
            this._queue = queue;
            this._service = twittService;
            this._logger = logger;
        }

        /// <inheritdoc/>
        public async Task ProcessTwittsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (this._queue.Count() == 0)
                    {
                        await Task.Delay(500, cancellationToken);
                        continue;
                    }
                    var twitt = _queue.GetTwitt();
                    if (twitt != null)
                        await this._service.AddTwittAsync(twitt);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "Error in Twitter Processing");
                }
            }
        }
    }
}