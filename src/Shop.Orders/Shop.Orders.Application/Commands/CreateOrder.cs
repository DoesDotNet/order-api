using MediatR;
using System;

namespace Shop.Orders.Application.Commands
{
    public class CreateOrder : IRequest
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        public CreateOrder(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
