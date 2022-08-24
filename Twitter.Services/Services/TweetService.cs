using AutoMapper;
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Twitter.Service.Models.Twitter;
using Twitter.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace Twitter.Service.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly IHashTagRepository _hashTagRepository;
        private readonly IMapper _mapper;

        public TweetService( ITweetRepository tweetRepository, IHashTagRepository hashTagRepository, IMapper mapper)
        {
            this._tweetRepository = tweetRepository;
            this._hashTagRepository = hashTagRepository;
            this._mapper = mapper;
        }

        /// <inheritdoc/>
        public void AddTweetAsync(TweetInfo tweet)
        {
            var tweetEntity = this._mapper.Map<Tweet>(tweet);
            this._tweetRepository.Add(tweetEntity);

            foreach (var hash in tweetEntity.HashTags)
                this._hashTagRepository.Add(hash);
        }
    }
}