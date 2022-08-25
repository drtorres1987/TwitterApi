using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twitter.Service.Services;

namespace TwitterConsole.Test.Services
{
    [TestFixture()]
    public class ConcurrentQueueManagerTest
    {
        private ILogger<ConcurrentQueueManager> logger;
        private ConcurrentQueueManager manager;

        [SetUp]
        public void Setup()
        {            
            logger = Mock.Of<ILogger<ConcurrentQueueManager>>();
            manager = new ConcurrentQueueManager(logger);
        }

        [Test]
        public void When_Count_Tweet()
        {
            var count = manager.Count();
            Assert.AreEqual(count, 0);
        }

        [Test]
        public void When_Add_Tweet()
        {
            manager.AddTweet("first");
            var count = manager.Count();
            Assert.AreEqual(count, 1);            
        }

        [Test]
        public void When_Get_Tweet()
        {
            manager.AddTweet("first");
            var tweet = manager.GetTweet();
            Assert.IsNull(tweet);
        }
    }
}
