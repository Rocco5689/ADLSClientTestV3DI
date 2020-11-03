using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.DataLake.Store;
using System.Net;

[assembly: FunctionsStartup(typeof(AdlsClientTestV3DI.Startup))]

namespace AdlsClientTestV3DI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ServicePointManager.DefaultConnectionLimit = 20;

            builder.Services.AddSingleton<IMyLoggerProvider, MyLoggerProvider>();
            builder.Services.AddSingleton<IAdlsClientClass, AdlsClientClass>();
        }
    }
}
