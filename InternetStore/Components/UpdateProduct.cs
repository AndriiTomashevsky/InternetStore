using InternetStore.Models;
using InternetStore.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InternetStore.Components
{
    public class UpdateProduct : ViewComponent
    {
        ICategoryRepository categoryRepository;
        IProductRepository productRepository;

        public UpdateProduct(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
        }

        public IViewComponentResult Invoke(long productId, string modalId)
        {
            ViewBag.ModalId = modalId;
            ViewBag.Categories = categoryRepository.Categories;

            return View(productId == 0 ? new Product() : productRepository.GetProduct(productId));
        }
    }
}
