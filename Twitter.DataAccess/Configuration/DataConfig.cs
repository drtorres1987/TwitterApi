using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.DataAccess.Configuration
{
  public static class DataConfig
    {
        public static IServiceCollection Configuration(this IServiceCollection services)
        {
            return services;
        }
    }
}
