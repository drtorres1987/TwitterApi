using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITwitterConsumerService
    {
        /// <summary>
        /// Consumer Process, this Process reads from Twitter Stream
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token to stop the process</param>
        /// <returns>Task</returns>
        Task ConsumeAsync(CancellationToken cancellationToken);
    }
}