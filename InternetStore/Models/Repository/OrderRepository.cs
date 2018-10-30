using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InternetStore.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        AppDbContext appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void AddOrder(Order order)
        {
            Order foundOrder = appDbContext.Orders.Where(item => item.OrderId == order.OrderId).FirstOrDefault();

            if (foundOrder != null)
            {
                appDbContext.AttachRange(order.Lines.Select(item => item));
                Order orderChanged = appDbContext.Orders.Where(item => item.OrderId == order.OrderId).FirstOrDefault();
                orderChanged.Shipping = true;
                appDbContext.SaveChanges();
            }

            appDbContext.AttachRange(order.Lines.Select(item => item.Product));
            appDbContext.Orders.Add(order);
            appDbContext.SaveChanges();
        }

        public IQueryable<Order> GetOrders()
        {
            return appDbContext.Orders.Include(order => order.Lines).ThenInclude(cartLine => cartLine.Product);
        }
    }
}
