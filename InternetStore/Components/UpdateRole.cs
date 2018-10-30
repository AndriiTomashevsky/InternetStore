using InternetStore.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InternetStore.Components
{
    public class UpdateRole : ViewComponent
    {
        RoleManager<AppRole> roleManager;

        public UpdateRole(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IViewComponentResult Invoke(AppRole appRole, string modalId)
        {
            ViewBag.ModalId = modalId;

            if (appRole == null)
            {
                return View(new AppRole());
            }
            else
            {
                return View(appRole);
            }
        }
    }
}
