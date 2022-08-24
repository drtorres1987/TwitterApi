
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Twitter.DataAccess.Repositories
{
    public class TwittRepository : ITwittRepository
    {
        private ConcurrentBag<Twitt> twitts;

        public TwittRepository()
        {
            twitts = new ConcurrentBag<Twitt>();
        }

        public void Add(Twitt entity)
        {
            twitts.Add(entity);
        }

        public int TotalCount()
        {
            return twitts.Count;
        }


    }
}