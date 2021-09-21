using Shop.Orders.Application.Data;
using Shop.Orders.Application.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Orders.Repository.InMemory
{
    public class InMemoryRepository : IRepository
    {
        private static Dictionary<Guid, Order> Orders = new Dictionary<Guid, Order>();

        public Task<Order> Get(Guid id)
        {
            if (Orders.ContainsKey(id))
            {
                return Task.FromResult(Orders[id]);
            }

            return Task.FromResult(null as Order);
        }

        public Task Save(Order order)
        {
            if (Orders.ContainsKey(order.Id))
            {
                Orders[order.Id] = order;
            }
            else
            {
                Orders.Add(order.Id, order);
            }

            return Task.CompletedTask;
        }
    }
}
