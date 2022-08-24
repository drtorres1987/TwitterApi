using Twitter.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Client.Interfaces;

namespace Twitter.Service.Services
{
    public class TwitterRetrieveService : ITwitterConsumerService
    {
        private readonly ITweetAPI _twitterClient;
        private readonly ITwitterQueueManager _queue;
        private readonly ILogger _logger;

        public TwitterRetrieveService(ILogger<TwitterRetrieveService> logger, ITweetAPI twitterClient, ITwitterQueueManager queue)
        {
            this._twitterClient = twitterClient;
            this._queue = queue;
            this._logger = logger;
        }

        /// <inheritdoc/>
        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            await foreach (var tweet in this._twitterClient.GetTwittsAsync(cancellationToken).WithCancellation(cancellationToken))
            {
                _queue.AddTwitt(tweet);
            }
        }
    }
}