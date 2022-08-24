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
            config = new MapperConfiguration(cfg => cfg.AddProfile<TwittProfile>());
            config.AssertConfigurationIsValid();

            mapper = config.CreateMapper();
        }

        [Test]
        public void Test_Twitt_Mapper()
        {
            var tag = "TagTest";
            var twitt = "Test";
            var twittINfo = new TwittInfo()
            {
                Data = new TwitInfoData()
                {
                    Text = twitt,
                    Entities = new TwitInfoEntity()
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

            var actual = mapper.Map<Twitt>(twittINfo);

            // Arrange
            Assert.AreEqual(actual.Text, twittINfo.Data.Text);
        }
    }
}