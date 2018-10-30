using InternetStore.Models;
using InternetStore.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace InternetStore.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderRepository;
        Cart cart;

        public OrderController(IOrderRepository orderRepository, Cart cart)
        {
            this.orderRepository = orderRepository;
            this.cart = cart;
        }

        public ViewResult Completed()
        {
            return View("Checkout");
        }

        [HttpGet]
        public RedirectToActionResult Checkout(string returnUrl)
        {
            if (cart.Lines.Count() == 0)
            {
                bool IsCartEmpty = true;
                return RedirectToAction("Index", "Cart", new { returnUrl, IsCartEmpty });
            }
            else
            {
                return RedirectToAction("Completed");
            }
        }

        [HttpPost]
        public ViewResult Checkout(Order order)
        {
            if (ModelState.IsValid && cart.Lines.Count() != 0)
            {
                order.Lines = cart.Lines.ToArray();
                orderRepository.AddOrder(order);
                cart.Clear();

                return View("Completed");
            }
            else
            {
                return View("Checkout");
            }
        }

        [Authorize(Roles = "Employees,Admins")]
        public ViewResult List()
        {
            ViewBag.IsOrder = true;
            var orders = orderRepository.GetOrders();
            return View(orders);
        }

        [Authorize(Roles = "Employees,Admins")]
        public IActionResult Ship(int orderId)
        {
            Order order = orderRepository.GetOrders().Where(item => item.OrderId == orderId).FirstOrDefault();
            order.Shipping = true;
            orderRepository.AddOrder(order);

            return RedirectToAction(nameof(List));
        }
    }
}