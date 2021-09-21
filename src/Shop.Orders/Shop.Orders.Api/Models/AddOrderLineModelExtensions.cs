using Shop.Orders.Application.Commands;
using System;

namespace Shop.Orders.Api.Models
{
    public static class AddOrderLineModelExtensions
    {
        public static AddOrderLine ToCommand(this AddOrderLineModel model, Guid orderId)
        {
            return new AddOrderLine(orderId, model.Sku, model.Quanity, model.ItemPrice);
        }
    }
}
