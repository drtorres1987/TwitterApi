using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.DataAccess.Dtos;
using Twitter.DataAccess.Repositories.Interfaces;
using Twitter.Service.Services;
using Twitter.Service.Services.Interfaces;

namespace TwitterConsole.Test.Services
{
    [TestFixture()]
    public class HashTagServiceTests
    {
        private IHashTagRepository hashTagRepository;
        private ITweetRepository tweetRepository;
        private IHashTagService reportService;

        [SetUp]
        public void Setup()
        {
            tweetRepository = Mock.Of<ITweetRepository>();
            hashTagRepository = Mock.Of<IHashTagRepository>();
            reportService = new HashTagService(hashTagRepository, tweetRepository);
        }

        [Test]
        public async Task When_HashTag_Returned()
        {
            int total = 10;
            var test = new List<HashTagCount>() { new HashTagCount() };
            Mock.Get(hashTagRepository)
                .Setup(s => s.TopHashTags(It.IsAny<int>()))
                .Returns(test);
            Mock.Get(tweetRepository)
                .Setup(s => s.TotalCount())
                .Returns(total);
            var result = await reportService.GetHashTags(2);

            // Assert
            Assert.AreEqual(result.TotalTwitts, total);
            Mock.VerifyAll();
        }
    }
}