using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITweetProcessingService
    {        
        Task ProcessTweetsAsync(CancellationToken cancellationToken);
    }
}