using MediatR;
using Shop.Orders.Application.Data;
using Shop.Orders.DataViews;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Orders.Application.Queries
{
    public class GetOrderHandler : IRequestHandler<GetOrder, OrderDataView>
    {
        private readonly IRepository _repository;

        public GetOrderHandler(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<OrderDataView> Handle(GetOrder query, CancellationToken cancellationToken)
        {
            var order = await _repository.Get(query.Id);
            if (order == null)
                return null;

            return new OrderDataView
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Lines = order.OrderLines.Select(x => new OrderLineDataView
                {
                    Sku = x.Sku,
                    Quanity = x.Quanity
                })
            };
        }
    }
}
