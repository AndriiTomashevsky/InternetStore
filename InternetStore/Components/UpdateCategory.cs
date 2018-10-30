using InternetStore.Models;
using InternetStore.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InternetStore.Components
{
    public class UpdateCategory : ViewComponent
    {
        ICategoryRepository categoryRepository;

        public UpdateCategory(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke(long categoryId, string modalId)
        {
            ViewBag.ModalId = modalId;

            return View(categoryId == 0 ? new Category() : categoryRepository.GetCategory(categoryId));
        }
    }
}
