using InternetStore.Models;
using InternetStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetStore.Components
{
    public class SearchForm : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new ProductsListViewModel());
        }
    }
}
