using InternetStore.Models;
using InternetStore.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InternetStore.Controllers
{
    public class CartController : Controller
    {
        IProductRepository productRepository;
        Cart cart;

        public CartController(IProductRepository productRepository, Cart cart)
        {
            this.productRepository = productRepository;
            this.cart = cart;
        }

        public ViewResult Index(string returnUrl, bool IsCartEmpty)
        {
            if (IsCartEmpty)
            {
                TempData["emptyCartMessage"] = "Your cart is empty";
            }

            ViewBag.Total = cart.Lines.Sum(item => item.Quantity * item.Product.Price);
            ViewBag.ReturnUrl = returnUrl;

            return View(cart.Lines);
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = productRepository.Products.Where(item => item.ProductId == productId).FirstOrDefault();
            cart.AddItem(product);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult Remove(int productId, string returnUrl)
        {
            cart.RemoveLine(productId);

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}