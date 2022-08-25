using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Twitter.Client.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Twitter.Service.Services.Interfaces;
using Twitter.Service.Services;
using Microsoft.Extensions.Logging;

namespace TwitterConsole.Test.Services
{
    [TestFixture()]
    public class TwitterRetrieveServiceTest
    {
        private ITweetAPI twitterClient;
        private ITwitterQueueManager queue;
        private ILogger<TwitterRetrieveService> logger;
        private ITwitterConsumerService twitterRetrieveService;

        [SetUp]
        public void Setup()
        {
            twitterClient = Mock.Of<ITweetAPI>();
            queue = Mock.Of <ITwitterQueueManager>();
            logger = Mock.Of<ILogger<TwitterRetrieveService>>();
            twitterRetrieveService = new TwitterRetrieveService(logger, twitterClient, queue);
        }

        [Test]
        public async Task When_Tweets_Added()
        {
            var token = new CancellationToken();
            var tweets = GetStringsAsync();
            
            Mock.Get(queue)
                .Setup(s => s.AddTweet(It.IsAny<string>()));

            Mock.Get(twitterClient)
                .Setup(s => s.GetTweetsAsync(It.IsAny<CancellationToken>()))
                .Returns(tweets);

            await twitterRetrieveService.ConsumeAsync(token);

            Mock.VerifyAll();
        }

        private static async IAsyncEnumerable<string> GetStringsAsync()
        {
            yield return "first";
            await Task.Delay(1000);
            yield return "second";
            await Task.Delay(1000);
            yield return "third";
        }
    }
}
