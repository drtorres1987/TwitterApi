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
        private readonly ITwiitAPI _twittClient;
        private readonly ITwitterQueueManager _queue;

        public TwitterRetrieveService(ILogger<TwitterRetrieveService> logger, ITwiitAPI twittClient, ITwitterQueueManager queue)
        {
            this._twittClient = twittClient;
            this._queue = queue;
        }

        /// <inheritdoc/>
        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            await foreach (var twitt in this._twittClient.GetTwittsAsync(cancellationToken).WithCancellation(cancellationToken))
            {
                _queue.AddTwitt(twitt);
            }
        }
    }
}