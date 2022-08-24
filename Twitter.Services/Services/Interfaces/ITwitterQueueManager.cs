using Twitter.Service.Models.Twitter;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITwitterQueueManager
    {
        /// <summary>
        /// Enqueue a Json string
        /// </summary>
        /// <param name="tweet"></param>
        void AddTwitt(string tweet);

        /// <summary>
        /// Dequeue the item and converts it to the Target Type
        /// </summary>
        /// <returns>The Item being dequeued</returns>
        TweetInfo GetTwitt();

        /// <summary>
        /// Queue Count
        /// </summary>
        /// <returns>Number of items in the queue</returns>
        int Count();
    }
}