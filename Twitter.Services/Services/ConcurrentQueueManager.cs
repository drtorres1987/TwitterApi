﻿using Twitter.Service.Models.Twitter;
using Twitter.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Twitter.Service.Services
{
    public class ConcurrentQueueManager : ITwitterQueueManager
    {
        public readonly ILogger<ConcurrentQueueManager> _logger;

        public ConcurrentQueueManager(ILogger<ConcurrentQueueManager> logger)
        {
            this._logger = logger;
        }

        private ConcurrentBag<string> queue = new ConcurrentBag<string>();

        /// <inheritdoc/>
        public int Count()
        {
            return queue.Count;
        }

        /// <inheritdoc/>
        public TwittInfo DeQueueTwitt()
        {
            if (this.queue.TryTake(out string result))
            {
                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                options.Converters.Add(new JsonStringEnumConverter());

                var setting = new JsonSerializerSettings()
                {
                    Error = (se, ev) =>
                    {
                        ev.ErrorContext.Handled = true;
                        this._logger.LogError(ev.ErrorContext.Error.Message);
                    },
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                };
                var twitt = JsonConvert.DeserializeObject<TwittInfo>(result, setting);
                return twitt;
            }

            return null;
        }

        /// <inheritdoc/>
        public void EnqueueTwitt(string twitt)
        {
            this.queue.Add(twitt);
        }
    }
}