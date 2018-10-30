using InternetStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace InternetStore.Infrastructure
{
    public class SessionCart
    {
        public static Cart GetCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

            if (session.Keys.Contains("Cart"))
            {
                Cart cart = session.GetCartFromSession<Cart>("Cart");

                return cart;
            }
            else
            {
                Cart cart = new Cart();
                session.SetCartToSession("Cart", cart);

                return cart;
            }
        }
    }
}
