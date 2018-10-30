using InternetStore.Models.Identity;
using InternetStore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace InternetStore.Components
{
    public class UpdateUser : ViewComponent
    {
        UserManager<AppUser> userManager;
        RoleManager<AppRole> roleManager;

        public UpdateUser(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId, string modalId)
        {
            ViewBag.ModalId = modalId;
            ViewBag.Roles = roleManager.Roles;
            ViewBag.RolesCount = roleManager.Roles.Count();

            if (userId == null)
            {
                return View("AddUser", new CreateModel());
            }
            else
            {
                return View(await userManager.FindByIdAsync(userId));
            }
        }
    }
}
