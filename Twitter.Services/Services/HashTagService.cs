﻿using Twitter.DataAccess.Repositories.Interfaces;
using Twitter.Service.Models.Report;
using Twitter.Service.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter.Service.Services
{
    public class HashTagService : IHashTagService
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ITwittRepository _twittRepository;

        public HashTagService(IHashTagRepository hasTagRepository, ITwittRepository twittRepository)
        {
            this._hashTagRepository = hasTagRepository;
            this._twittRepository = twittRepository;
        }

        /// <inheritdoc/>
        public async Task<HashTagReport> GetHashTags(int number)
        {
            var result = new HashTagReport
            {
                TotalTwitts = this._twittRepository.TotalCount()
            };
            var hasTags = this._hashTagRepository.TopHashTags(number);
            result.HashTags = hasTags.Select(c => new HashTagRecord()
            {
                Tag = c.Tag,
                NumberOfReferences = c.Count
            });
            return await Task.FromResult(result);
        }


    }
}