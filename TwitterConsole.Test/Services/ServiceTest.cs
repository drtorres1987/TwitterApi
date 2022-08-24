using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Twitter.DataAccess.UnitOfWork;
using Twitter.Service.Models.Twitter;
using Twitter.Service.Services;
using Twitter.Service.Services.Interfaces;

namespace TwitterConsole.Test.Services
{
    [TestFixture()]
    public class ServiceTest
    {
        private ITwittRepository _twittRepository;
        private IHashTagRepository _hashTagRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ITwittService _twittService;

        [SetUp]
        public void Setup()
        {

            _unitOfWork = Mock.Of<IUnitOfWork>();
            _hashTagRepository = Mock.Of<IHashTagRepository>();
            _twittRepository = Mock.Of<ITwittRepository>();
            _mapper = Mock.Of<IMapper>();
            this._twittService = new TwittService(_unitOfWork, _twittRepository, _hashTagRepository, _mapper);
        }

        [Test]
        public async Task When_AddTwittAsync()
        {
            var tag = "TagTest";
            var twitt = "Test";           

            var twit = new Twitt()
            {
                Text = twitt,
                HashTags = new List<Twitter.DataAccess.Entities.HashTag>()
                {
                    new Twitter.DataAccess.Entities.HashTag()
                    {
                        Tag = tag
                    }
                }
            };            

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
                        }
                    }
                }
            };
            Mock.Get(_hashTagRepository)
                .Setup(s => s.Add(It.IsAny<Twitter.DataAccess.Entities.HashTag>()));

            Mock.Get(_unitOfWork)
                .Setup(s => s.CommitChangesAsync());



            Mock.Get(_mapper)
                .Setup(s => s.Map<Twitt>(It.IsAny<TwittInfo>()))
                .Returns(twit);

            Mock.Get(_twittRepository)
                .Setup(s => s.Add(It.IsAny<Twitt>()));
            

            await _twittService.AddTwittAsync(twittINfo);

            // Assert
            Mock.VerifyAll();
        }
    }
}