
using Twitter.DataAccess.Entities;
using Twitter.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Twitter.DataAccess.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private ConcurrentBag<Tweet> tweets;

        public TweetRepository()
        {
            tweets = new ConcurrentBag<Tweet>();
        }

        public void Add(Tweet entity)
        {
            tweets.Add(entity);
        }

        public int TotalCount()
        {
            return tweets.Count;
        }


    }
}