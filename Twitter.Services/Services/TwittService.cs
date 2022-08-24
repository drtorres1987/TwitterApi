using AutoMapper;
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Twitter.Service.Models.Twitter;
using Twitter.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace Twitter.Service.Services
{
    public class TwittService : ITwittService
    {
        private readonly ITwittRepository _twittRepository;
        private readonly IHashTagRepository _hashTagRepository;
        private readonly IMapper _mapper;

        public TwittService( ITwittRepository twittRepository, IHashTagRepository hashTagRepository, IMapper mapper)
        {
            this._twittRepository = twittRepository;
            this._hashTagRepository = hashTagRepository;
            this._mapper = mapper;
        }

        /// <inheritdoc/>
        public void AddTwittAsync(TwittInfo twitt)
        {
            var twittEntity = this._mapper.Map<Twitt>(twitt);
            this._twittRepository.Add(twittEntity);

            foreach (var hash in twittEntity.HashTags)
                this._hashTagRepository.Add(hash);
        }
    }
}