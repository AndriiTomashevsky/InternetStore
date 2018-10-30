using InternetStore.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InternetStore.Components
{
    public class NavigationMenu : ViewComponent
    {
        ICategoryRepository repository;

        public NavigationMenu(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            string s = (string)RouteData.Values["category"];
            ViewBag.SelectedCategory = RouteData.Values["category"];

            return View(repository.Categories);
        }
    }
}
