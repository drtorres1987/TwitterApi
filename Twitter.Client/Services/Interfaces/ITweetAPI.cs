using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Twitter.Client.Interfaces
{
    public interface ITweetAPI
    {
        IAsyncEnumerable<string> GetTweetsAsync( CancellationToken cancellationToken = default);
    }
}