using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITwitterConsumerService
    {
        Task ConsumeAsync(CancellationToken cancellationToken);
    }
}