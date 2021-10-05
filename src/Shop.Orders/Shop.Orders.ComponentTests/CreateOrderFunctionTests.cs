using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Orders.Api;
using Shop.Orders.Application.Data;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Orders.ComponentTests
{
    public class CreateOrderFunctionTests : IClassFixture<ApplicationFixture>
    {
        private ApplicationFixture _fixture;
        private CreateOrderFunction _sut;

        public CreateOrderFunctionTests(ApplicationFixture fixture)
        {
            _fixture = fixture;
            _sut = fixture.GetService<CreateOrderFunction>();
        }

        [Fact]
        public async Task CreateOrderWithInvalidIdReturns400()
        {
            // Arrange
            string body = "{\"id\":\"00000000-0000-0000-0000-000000000000\", \"customerId\": \"b13c0bc2-d4c6-4e34-9c72-ea299e185b9d\"}";
            var bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(body));

            var request = new DefaultHttpContext().Request;
            request.Body = bodyStream;

            // Act
            var result = await _sut.Run(request, CancellationToken.None);
            var badRequestObjectResult = result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(badRequestObjectResult);
            Assert.Equal(400, badRequestObjectResult.StatusCode);
        }

        [Fact]
        public async Task CreateOrderWithInvalidCustomerIdReturns400()
        {
            // Arrange
            string body = "{\"id\":\"733b43eb-57cf-4e2f-870b-b7954f9cbd00\", \"customerId\": \"00000000-0000-0000-0000-000000000000\"}";
            var bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(body));

            var request = new DefaultHttpContext().Request;
            request.Body = bodyStream;

            // Act
            var result = await _sut.Run(request, CancellationToken.None);
            var badRequestObjectResult = result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(badRequestObjectResult);
            Assert.Equal(400, badRequestObjectResult.StatusCode);
        }

        [Fact]
        public async Task CreateOrderWithValidRequestReturn201()
        {
            // Arrange
            string body = "{\"id\":\"733b43eb-57cf-4e2f-870b-b7954f9cbd00\", \"customerId\": \"b13c0bc2-d4c6-4e34-9c72-ea299e185b9d\"}";
            var bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(body));

            var request = new DefaultHttpContext().Request;
            request.Body = bodyStream;

            // Act
            var result = await _sut.Run(request, CancellationToken.None);
            var statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(201, statusCodeResult.StatusCode);

            var repository = _fixture.GetService<IRepository>();
            var order = await repository.Get(new Guid("733b43eb-57cf-4e2f-870b-b7954f9cbd00"));

            Assert.NotNull(order);
            Assert.Equal(new Guid("733b43eb-57cf-4e2f-870b-b7954f9cbd00"), order.Id);
            Assert.Equal(new Guid("b13c0bc2-d4c6-4e34-9c72-ea299e185b9d"), order.CustomerId);
        }
    }
}
