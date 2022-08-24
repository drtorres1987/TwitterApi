using Twitter.DataAccess.Dtos;
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Twitter.DataAccess.Repositories
{
    public class HashTagRepository : IHashTagRepository
    {
        private readonly ConcurrentBag<HashTag> hashTags;       

        public HashTagRepository()
        {
            hashTags = new ConcurrentBag<HashTag>();
        }

        public List<HashTagCount> TopHashTags(int number)
        {
            var result = this.hashTags
                .GroupBy(x => x.Tag)
                .Select(x => new HashTagCount() { Tag = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .Take(number)
                .ToList();
            return result;
        }        

        public void Add(HashTag entity)
        {
            hashTags.Add(entity);
        }
    }
}