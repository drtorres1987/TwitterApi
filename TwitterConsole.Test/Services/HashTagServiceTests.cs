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
        private IHashTagService hashTagService;
        private IHashTagRepository hashTagRepository;
        private ITwittRepository twittRepository;
        

        [SetUp]
        public void Setup()
        {
            hashTagRepository = Mock.Of<IHashTagRepository>();
            twittRepository = Mock.Of<ITwittRepository>();
            hashTagService = new HashTagService(hashTagRepository, twittRepository);
        }

        [Test]
        public async Task When_HashTag_Returned()
        {            
            int total = 10;
            var hashTagsTest = new List<HashTagCount>(){ new HashTagCount() };

            Mock.Get(hashTagRepository)
                .Setup(s => s.TopNAsync(It.IsAny<int>()))
                .ReturnsAsync(hashTagsTest);
            Mock.Get(twittRepository)
                .Setup(s => s.TotalCount())
                .ReturnsAsync(total);
            
            var result = await hashTagService.GetHashTags(2);

            // Assert
            Assert.AreEqual(result.TotalTwitts, total);
            Mock.VerifyAll();
        }
    }
}