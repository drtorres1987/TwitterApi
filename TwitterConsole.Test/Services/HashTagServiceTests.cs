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
        private ITwittRepository twittRepository;
        private IHashTagService reportService;

        [SetUp]
        public void Setup()
        {
            twittRepository = Mock.Of<ITwittRepository>();
            hashTagRepository = Mock.Of<IHashTagRepository>();
            reportService = new HashTagService(hashTagRepository, twittRepository);
        }

        [Test]
        public async Task When_HashTag_Returned()
        {
            int total = 10;
            var test = new List<HashTagCount>() { new HashTagCount() };
            Mock.Get(hashTagRepository)
                .Setup(s => s.TopNAsync(It.IsAny<int>()))
                .ReturnsAsync(test);
            Mock.Get(twittRepository)
                .Setup(s => s.TotalCount())
                .ReturnsAsync(total);
            var result = await reportService.GetHashTags(2);

            // Assert
            Assert.AreEqual(result.TotalTwitts, total);
            Mock.VerifyAll();
        }
    }
}