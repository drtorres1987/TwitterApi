using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITwittProcessingService
    {
        /// <summary>
        /// Process the Twitts being inserted into the Queue b
        /// </summary>
        /// <param name="cancellationToken">Token to Stop the processing</param>
        /// <returns>Task</returns>
        Task ProcessTwittsAsync(CancellationToken cancellationToken);
    }
}