using AutoMapper;
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Twitter.DataAccess.UnitOfWork;
using Twitter.Service.Models.Twitter;
using Twitter.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace Twitter.Service.Services
{
    public class TwittService : ITwittService
    {
        private readonly ITwittRepository _twittRepository;
        private readonly IHashTagRepository _hashTagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TwittService(IUnitOfWork unitOfWork, ITwittRepository twittRepository, IHashTagRepository hashTagRepository, IMapper mapper)
        {
            this._twittRepository = twittRepository;
            this._hashTagRepository = hashTagRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task AddTwittAsync(TwittInfo twitt)
        {
            var twittEntity = this._mapper.Map<Twitt>(twitt);
            this._twittRepository.Add(twittEntity);

            foreach (var hash in twittEntity.HashTags)
                this._hashTagRepository.Add(hash);

            await this._unitOfWork.CommitChangesAsync();
        }
    }
}