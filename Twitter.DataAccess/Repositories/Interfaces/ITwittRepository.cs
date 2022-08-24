using Twitter.DataAccess.Entities;
using System.Threading.Tasks;

namespace Twitter.DataAccess.Repositories.Interfaces
{
    public interface ITwittRepository : IRepository<Twitt>
    {
        public Task<int> TotalCount();
    }
}