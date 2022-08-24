using Twitter.Service.Models.Twitter;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITwitterQueueManager
    {
        /// <summary>
        /// Enqueue a Json string
        /// </summary>
        /// <param name="twitt"></param>
        void EnqueueTwitt(string twitt);

        /// <summary>
        /// Dequeue the item and converts it to the Target Type
        /// </summary>
        /// <returns>The Item being dequeued</returns>
        TwittInfo DeQueueTwitt();

        /// <summary>
        /// Queue Count
        /// </summary>
        /// <returns>Number of items in the queue</returns>
        int Count();
    }
}