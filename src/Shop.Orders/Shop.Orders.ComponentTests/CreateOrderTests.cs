using AzureFunctions.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Orders.Api;
using Shop.Orders.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Orders.ComponentTests
{
    public class CreateOrderTests
    {
        //readonly CreateOrderFunction _createOrderFunction;

        //public CreateOrderTests()
        //{
        //    var startup = new Startup();
        //    var host = new HostBuilder()
        //        .ConfigureWebJobs(startup.Configure)
        //        .Build();


        //    _createOrderFunction = new CreateOrderFunction(
        //        host.Services.GetRequiredService<ILogger<CreateOrderFunction>>(), 
        //        host.Services.GetRequiredService<IMediator>());
        //}



        [Fact]
        public async Task Create_order_returns_201()
        {
            var model = new CreateOrderModel
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };

            // Arrange
            object response = null;
            using (var host = new HostBuilder()
                .ConfigureWebJobs(builder => builder
                    .AddHttp(options => options.SetResponse = (request, o) => response = o))
                .Build())
            {
                await host.StartAsync();
                var jobs = host.Services.GetService<IJobHost>();

                // Act
                await jobs.CallAsync(nameof(CreateOrderFunction), new Dictionary<string, object>
                {
                    ["request"] = new DummyHttpRequest("{\"id\":\"733b43eb-57cf-4e2f-870b-b7954f9cbd00\",\"customerId\": \"a13c0bc2-d4c6-4e34-9c72-ea299e185b9d\"}")
                });
            }

            // Assert
            Assert.IsType<StatusCodeResult>(response);
          
        }
    }
}
