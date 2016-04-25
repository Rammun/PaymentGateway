using PaymentGateway.DAL.Interfaces;
using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.DAL.Implementations
{
    public class FakeOrderRepository : IOrderRepository
    {
        List<Order> orders;
        ReadOnlyCollection<Order> ordersReadOnly;

        public FakeOrderRepository()
        {
            orders = new List<Order>()
            {
                new Order { Amount = 1000 },
                new Order { Amount = 20000 },
                new Order { Amount = 300000 },
                new Order { Amount = 4000000 },
                new Order { Amount = 50000000 },
            };
            ordersReadOnly = new ReadOnlyCollection<Order>(orders);
        }

        public IEnumerable<Order> GetAll()
        {
            return ordersReadOnly;
        }

        public Order GetById(int id)
        {
            return orders.Find(o => o.Id == id);
        }

        public void Add(Order order)
        {
            if (order == null)
                throw new NullReferenceException("Параметр order равен null");            
            orders.Add(order);
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var order = orders.Find(o => o.Id == id);
            if (order == null)
                throw new Exception("Попытка удалить несуществующий заказ");
            orders.Remove(order);
        }
    }
}
