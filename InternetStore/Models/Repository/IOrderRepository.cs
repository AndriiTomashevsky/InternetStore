using System.Linq;

namespace InternetStore.Models.Repository
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrders();
        void AddOrder(Order order);
    }
}
