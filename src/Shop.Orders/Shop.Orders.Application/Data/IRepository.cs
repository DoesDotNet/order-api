using Shop.Orders.Application.Domain;
using System;
using System.Threading.Tasks;

namespace Shop.Orders.Application.Data
{
    public interface IRepository
    {
        Task Save(Order order);
        Task<Order> Get(Guid id);
    }
}
