using InternetStore.Infrastructure;
using InternetStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InternetStore.Components
{
    public class CartSummary : ViewComponent
    {
        Cart cart;

        public CartSummary(Cart cart)
        {
            this.cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
