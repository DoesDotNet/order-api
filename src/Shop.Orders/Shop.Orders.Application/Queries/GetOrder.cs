using MediatR;
using Shop.Orders.DataViews;
using System;

namespace Shop.Orders.Application.Queries
{
    public class GetOrder : IRequest<OrderDataView>
    {
        public Guid Id { get; }

        public GetOrder(Guid id)
        {
            Id = id;
        }
    }
}
