using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Shop.Orders.Api;
using Shop.Orders.Application.Data;
using Shop.Orders.Repository.InMemory;
using System;
using System.Collections.Generic;

namespace Shop.Orders.ComponentTests
{
    public class ApplicationFixture : IDisposable
    {
        private IHost _host;

        public ApplicationFixture()
        {
            var config = new Dictionary<string, string>
            {
                ["AzureWebJobsStorage"] = "UseDevelopmentStorage=true",
                ["FUNCTIONS_WORKER_RUNTIME"] = "dotnet"
            };

            _host = new HostBuilder()
                .ConfigureWebJobs(builder =>
                {
                    builder.UseWebJobsStartup(typeof(Startup), new WebJobsBuilderContext(), NullLoggerFactory.Instance);
                })
                .ConfigureServices(services =>
                {
                    services.AddScoped<CreateOrderFunction>();
                    RegisterMocks(services);
                })
                .ConfigureAppConfiguration((_, configBuilder) => configBuilder.AddInMemoryCollection(config))
                .Build();

            _host.Start();
        }

        public T GetService<T>()
        {
            return _host.Services.GetRequiredService<T>();
        }


        private void RegisterMocks(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Scoped(typeof(IRepository), typeof(InMemoryRepository)));
        }


        public void Dispose()
        {
            _host.StopAsync().Wait();
        }
    }
}
