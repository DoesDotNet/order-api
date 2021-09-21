using MediatR;
using System;

namespace Shop.Orders.Application.Commands
{
    public class AddOrderLine : IRequest
    {
        public Guid OrderId { get; }
        public string Sku { get; }
        public int Quanity { get; }
        public double ItemPrice { get; }

        public AddOrderLine(Guid orderId, string sku, int quanity, double itemPrice)
        {
            OrderId = orderId;
            Sku = sku;
            Quanity = quanity;
            ItemPrice = itemPrice;
        }
    }
}
