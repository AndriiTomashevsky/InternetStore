using InternetStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetStore.Components
{
    public class Login : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new LoginModel());
        }
    }
}
