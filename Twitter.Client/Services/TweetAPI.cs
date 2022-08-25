using Twitter.Client.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using Twitter.Client.Interfaces;

namespace Twitter.Client.Services
{
    public class TweetAPI : ITweetAPI
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ClientConfiguration> options;

        public TweetAPI(HttpClient httpClient, IOptions<ClientConfiguration> options)
        {
            this._httpClient = httpClient;
            this.options = options;
        }

        public async IAsyncEnumerable<string> GetTweetsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Value.Token}");

            using var stream = await this._httpClient.GetStreamAsync($"{options.Value.Url}?tweet.fields=entities");
            using var streamReader = new StreamReader(stream);
            while (!streamReader.EndOfStream || cancellationToken.IsCancellationRequested)
            {
                yield return await streamReader.ReadLineAsync();
            }
        }
    }
}