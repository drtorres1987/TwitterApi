using Twitter.DataAccess.Context;
using Twitter.DataAccess.Dtos;
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter.DataAccess.Repositories
{
    public class HashTagRepository : BaseRepository<HashTag, Context.TwitterDBContext>, IHashTagRepository
    {
        public HashTagRepository(Context.TwitterDBContext context) : base(context)
        {
        }

        public async Task<List<HashTagCount>> TopNAsync(int topN)
        {
            var hashes = this.context.HashTags.Take(100).ToList();
            var result = await this.context.HashTags
                .GroupBy(x => x.Tag)
                .Select(x => new HashTagCount() { Tag = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .Take(topN)
                .ToListAsync();
            return result;
        }
    }
}