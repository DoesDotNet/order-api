using MediatR;
using Shop.Orders.Application.Data;
using Shop.Orders.Application.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Orders.Application.Commands
{
    public class CreateOrderHandler : AsyncRequestHandler<CreateOrder>
    {
        private readonly IRepository _repository;

        public CreateOrderHandler(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task Handle(CreateOrder command, CancellationToken cancellationToken)
        {
            var order = Order.Create(command.Id, command.CustomerId);

            await _repository.Save(order);
        }
    }
}
