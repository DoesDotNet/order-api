using MediatR;
using Shop.Orders.Application.Data;
using Shop.Orders.Application.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Orders.Application.Commands
{
    public class AddOrderLineHandler : AsyncRequestHandler<AddOrderLine>
    {
        private readonly IRepository _repository;

        public AddOrderLineHandler(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task Handle(AddOrderLine command, CancellationToken cancellationToken)
        {
            var order = await _repository.Get(command.OrderId);
            var orderLine = new OrderLine(command.Sku, command.Quanity, command.ItemPrice);

            order.AddOrderLine(orderLine);

            await _repository.Save(order);
        }
    }
}
