using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shop.Orders.Application.Domain
{
    public class Order
    {
        private List<OrderLine> _orderLines; 


        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public ReadOnlyCollection<OrderLine> OrderLines => _orderLines.AsReadOnly();

        public static Order Create(Guid id, Guid customerId)
        {
            return new Order(id, customerId);
        }

        private Order(Guid id, Guid customerId)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (customerId == Guid.Empty)
                throw new ArgumentOutOfRangeException(nameof(customerId));

            Id = id;
            CustomerId = customerId;
            _orderLines = new List<OrderLine>();
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            _orderLines.Add(orderLine);
        }
    }
}
