using Twitter.DataAccess.Context;
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Twitter.DataAccess.Repositories
{
    public class TwittRepository : BaseRepository<Twitt, Context.TwitterDBContext>, ITwittRepository
    {
        public TwittRepository(Context.TwitterDBContext context) : base(context)
        {
        }

        public async Task<int> TotalCount()
        {
            return await this.context.Twitts.CountAsync();
        }
    }
}