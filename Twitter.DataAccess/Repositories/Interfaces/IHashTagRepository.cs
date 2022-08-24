using Twitter.DataAccess.Dtos;
using Twitter.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twitter.DataAccess.Repositories.Interfaces
{
    public interface IHashTagRepository : IRepository<HashTag>
    {
        public List<HashTagCount> TopHashTags(int number);
    }
}