using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Twitter.Service.Models.Twitter;
using Twitter.Service.Services;
using Twitter.Service.Services.Interfaces;

namespace TwitterConsole.Test.Services
{
    [TestFixture()]
    public class TweetServiceTest
    {
        private ITweetRepository _tweetRepository;
        private IHashTagRepository _hashTagRepository;
        private IMapper _mapper;
        private ITweetService _tweetService;

        [SetUp]
        public void Setup()
        {

            _hashTagRepository = Mock.Of<IHashTagRepository>();
            _tweetRepository = Mock.Of<ITweetRepository>();
            _mapper = Mock.Of<IMapper>();
            this._tweetService = new TweetService( _tweetRepository, _hashTagRepository, _mapper);
        }

        [Test]
        public void When_AddTweetAsync()
        {
            var tag = "TagTest";
            var tweet = "Test";           

            var tweetObject = new Tweet()
            {
                Text = tweet,
                HashTags = new List<Twitter.DataAccess.Entities.HashTag>()
                {
                    new Twitter.DataAccess.Entities.HashTag()
                    {
                        Tag = tag
                    }
                }
            };            

            var tweetInfo = new TweetInfo()
            {
                Data = new TweetInfoData()
                {
                    Text = tweet,
                    Entities = new TweetInfoEntity()
                    {
                        HashTags = new List<Twitter.Service.Models.Twitter.HashTag>()
                        {
                            new Twitter.Service.Models.Twitter.HashTag()
                            {
                                Tag = tag
                            }
                        }
                    }
                }
            };
            Mock.Get(_hashTagRepository)
                .Setup(s => s.Add(It.IsAny<Twitter.DataAccess.Entities.HashTag>()));     

            Mock.Get(_mapper)
                .Setup(s => s.Map<Tweet>(It.IsAny<TweetInfo>()))
                .Returns(tweetObject);

            Mock.Get(_tweetRepository)
                .Setup(s => s.Add(It.IsAny<Tweet>()));
            

            _tweetService.AddTweetAsync(tweetInfo);

            // Assert
            Mock.VerifyAll();
        }
    }
}