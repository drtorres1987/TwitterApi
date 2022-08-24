using Twitter.DataAccess.Dtos;
using Twitter.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twitter.DataAccess.Repositories.Interfaces
{
    public interface IHashTagRepository : IRepository<HashTag>
    {
        Task<List<HashTagCount>> TopNAsync(int topN);
    }
}