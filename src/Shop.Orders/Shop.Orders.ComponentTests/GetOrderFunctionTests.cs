using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Orders.Api.Functions;
using Shop.Orders.Application.Data;
using Shop.Orders.Application.Domain;
using Shop.Orders.DataViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Orders.ComponentTests
{
    public class GetOrderFunctionTests : IClassFixture<ApplicationFixture>
    {
        private readonly ApplicationFixture _fixture;
        private readonly GetOrderFunction _sut;

        public GetOrderFunctionTests(ApplicationFixture fixture)
        {
            _fixture = fixture;
            _sut = fixture.GetService<GetOrderFunction>();
        }

        [Fact]
        public async Task GetOrderWithValidIdReturnsOrder()
        {
            // Arrange
            var request = new DefaultHttpContext().Request;
            Guid id = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();

            var repository = _fixture.GetService<IRepository>();
            var existingOrder = Order.Create(id, customerId);
            await repository.Save(existingOrder);


            // Act
            var result = await _sut.Run(request, id, CancellationToken.None);
            var okObjectResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okObjectResult);

            var orderResult = okObjectResult.Value as OrderDataView;
            Assert.NotNull(orderResult);
            Assert.Equal(id, orderResult.Id);
            Assert.Equal(customerId, orderResult.CustomerId);
        }

        [Fact]
        public async Task GetOrderWithInvalidIdReturns404()
        {
            // Arrange
            var request = new DefaultHttpContext().Request;
            Guid id = Guid.NewGuid();

            // Act
            var result = await _sut.Run(request, id, CancellationToken.None);
            var notFound = result as NotFoundResult;

            // Assert
            Assert.NotNull(notFound);
        }
    }
}
