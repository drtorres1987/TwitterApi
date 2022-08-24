using Twitter.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Twitter.DataAccess.Context
{
    public class TwitterDBContext : DbContext
    {
        public TwitterDBContext(DbContextOptions<TwitterDBContext> options) : base(options)
        {
        }

        public DbSet<Twitt> Twitts { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
    }
}