using Twitter.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Service.Services
{
    public class TweetProcessService : ITweetProcessingService
    {
        private readonly ITwitterQueueManager _queue;
        private readonly ITweetService _service;
        private readonly ILogger _logger;

        public TweetProcessService(ILogger<TweetProcessService> logger, ITwitterQueueManager queue, ITweetService tweetService)
        {
            this._queue = queue;
            this._service = tweetService;
            this._logger = logger;
        }

        /// <inheritdoc/>
        public async Task ProcessTweetsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (this._queue.Count() == 0)
                    {
                        await Task.Delay(200, cancellationToken);
                        continue;
                    }
                    var tweet = _queue.GetTweet();
                    if (tweet != null)
                        this._service.AddTweetAsync(tweet);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "Error in Twitter Processing");
                }
            }
        }
    }
}