using Twitter.Service.Models.Twitter;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITweetService
    {        
        void AddTweetAsync(TweetInfo tweet);
    }
}