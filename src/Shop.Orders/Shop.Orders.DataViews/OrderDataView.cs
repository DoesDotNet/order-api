using System;
using System.Collections.Generic;

namespace Shop.Orders.DataViews
{
    public class OrderDataView
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        public IEnumerable<OrderLineDataView> Lines {get;set;}
    }
}
