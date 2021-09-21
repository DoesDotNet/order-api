using System;

namespace Shop.Orders.Api.Models
{
    public class CreateOrderModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}
