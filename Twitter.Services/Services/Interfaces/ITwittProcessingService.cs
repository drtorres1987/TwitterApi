using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Service.Services.Interfaces
{
    public interface ITwittProcessingService
    {        
        Task ProcessTwittsAsync(CancellationToken cancellationToken);
    }
}