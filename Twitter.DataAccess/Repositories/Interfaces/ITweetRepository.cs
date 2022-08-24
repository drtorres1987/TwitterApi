using Twitter.DataAccess.Entities;
using System.Threading.Tasks;

namespace Twitter.DataAccess.Repositories.Interfaces
{
    public interface ITweetRepository : IRepository<Tweet>
    {
        public int TotalCount();
    }
}