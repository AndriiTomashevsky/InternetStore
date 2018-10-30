using InternetStore.Models;
using InternetStore.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InternetStore.Controllers
{
    [Authorize(Roles = "Managers,Admins")]
    public class ProductController : Controller
    {
        IProductRepository productRepository;
        int pageSize = 6;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            ViewBag.IsProduct = true;

            return View(productRepository.Products.Take(pageSize));
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.Length];
                    image.OpenReadStream().Read(product.ImageData, 0, (int)image.Length);
                }

                if (product.ProductId == 0)
                {
                    productRepository.AddProduct(product);
                    TempData.Add("Message", $"Product {product.Name} has been added");
                }
                else
                {
                    productRepository.UpdateProduct(product);
                    TempData.Add("Message", $"Product {product.Name} has been updated");
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(nameof(Index), productRepository.Products);
            }
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deleted = productRepository.Delete(productId);

            if (deleted != null)
            {
                TempData["message"] = $"Product {deleted.Name} has been deleted";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateAll(Product[] products)
        {
            productRepository.UpdateAll(products);
            TempData.Add("Message", "Edited prices have been updated");

            return RedirectToAction(nameof(Index));
        }
    }
}