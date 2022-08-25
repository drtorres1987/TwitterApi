using AutoMapper;
using NUnit.Framework;
using System.Collections.Generic;
using Twitter.DataAccess.Entities;
using Twitter.Service.Mapper;
using Twitter.Service.Models.Twitter;

namespace TwitterConsole.Test.AutoMapper.Service
{
    [TestFixture()]
    public class ProfileTests
    {
        private MapperConfiguration config;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {            
            config = new MapperConfiguration(cfg => cfg.AddProfile<TweetProfile>());
            config.AssertConfigurationIsValid();

            mapper = config.CreateMapper();
        }

        [Test]
        public void Test_Tweet_Mapper()
        {
            var tag = "TagTest";
            var tweet = "Test";
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
                        },
                        
                    }
                }
            };

            var actual = mapper.Map<Tweet>(tweetInfo);

            // Arrange
            Assert.AreEqual(actual.Text, tweetInfo.Data.Text);
        }
    }
}