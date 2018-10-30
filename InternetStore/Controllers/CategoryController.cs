using InternetStore.Models;
using InternetStore.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetStore.Controllers
{
    [Authorize(Roles = "Managers,Admins")]
    public class CategoryController : Controller
    {
        ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            ViewBag.IsCategory = true;

            return View(categoryRepository.Categories);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    categoryRepository.AddCategory(category);
                    TempData.Add("Message", $"Product {category.Name} has been added");
                }
                else
                {
                    categoryRepository.UpdateCategory(category);
                    TempData.Add("Message", $"Product {category.Name} has been updated");
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(nameof(Index), categoryRepository.Categories);
            }
        }

        [HttpPost]
        public IActionResult Delete(long categoryId)
        {
            Category deleted = categoryRepository.Delete(categoryId);

            if (deleted != null)
            {
                TempData["message"] = $"Category {deleted.Name} has been deleted";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
