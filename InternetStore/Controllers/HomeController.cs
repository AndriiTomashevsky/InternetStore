using Microsoft.AspNetCore.Mvc;
using InternetStore.Models.Repository;
using System.Linq;
using InternetStore.Models.ViewModels;
using InternetStore.Models;
using System;

namespace InternetStore.Controllers
{
    public class HomeController : Controller
    {
        IProductRepository repository;

        int pageSize = 4;

        public HomeController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(string category, string search, SortState? sortState = null, int page = 1)
        {
            ViewBag.Active = category == null ? "active" : null;

            IQueryable<Product> products = repository.Products;

            if (search != null)
            {
                decimal result;
                Decimal.TryParse(search, out result);
                products = products.Where(item => item.Name == search.ToString() || item.Airflow == result ||
                    item.Power == result);
            }

            switch (sortState)
            {
                case SortState.NameAsc:
                    products = products.OrderBy(item => item.Name);
                    break;
                case SortState.NameDesc:
                    products = products.OrderByDescending(item => item.Name);
                    break;
                case SortState.PriceAsc:
                    products = products.OrderBy(item => item.Price);
                    break;
                case SortState.PriceDesc:
                    products = products.OrderByDescending(item => item.Price);
                    break;
            }

            return View(new ProductsListViewModel
            {
                SortingInfo = new SortingInfo(sortState),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = page,
                    TotalItems = products.Where(item => category == null || item.Category.Name == category).Count(),
                },
                Products = products
                    .Where(item => category == null || item.Category.Name == category)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                Search = search,
                CurrentCategory = category
            });

        }

        public FileContentResult GetImage(int productId)
        {
            Product product = repository.Products.FirstOrDefault(item => item.ProductId == productId);

            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}