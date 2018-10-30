using InternetStore.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InternetStore.Controllers
{
    [Authorize(Roles = "Admins")]
    public class RoleAdminController : Controller
    {
        RoleManager<AppRole> roleManager;
        UserManager<AppUser> userManager;

        public RoleAdminController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.IsRole = true;

            return View(roleManager.Roles);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(string roleId, string name)
        {
            if (ModelState.IsValid)
            {
                AppRole appRole = await roleManager.FindByIdAsync(roleId);

                if (appRole == null)
                {
                    IdentityResult identityResult = await roleManager.CreateAsync(new AppRole(name));

                    if (identityResult.Succeeded)
                    {
                        TempData.Add("Message", $"Role {name} has been added");

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        AddErrorsFromResult(identityResult);
                    }
                }
                else
                {
                    appRole.Name = name;
                    TempData.Add("Message", $"Role {name} has been updated");
                    IdentityResult identityResult = await roleManager.UpdateAsync(appRole);

                    if (identityResult.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        AddErrorsFromResult(identityResult);
                    }

                }
            }
            return View(nameof(Index), roleManager.Roles);
        }

        public async Task<JsonResult> Validate(string name)
        {
            AppRole appRole = roleManager.FindByNameAsync(name).Result;

            if (appRole != null)
            {
                return Json($"Name '{name}' is already taken");
            }

            IdentityResult identityResult = await roleManager.RoleValidators[0].ValidateAsync(roleManager, new AppRole(name));

            if (identityResult.Succeeded)
            {
                return Json(true);
            }

            string description = null;
            foreach (var item in identityResult.Errors)
            {
                if (item.Description != null)
                {
                    description = item.Description;
                }
            }

            return Json(description);
        }

        public async Task<IActionResult> Delete(string roleId)
        {
            AppRole appRole = await roleManager.FindByIdAsync(roleId);

            if (appRole != null)
            {
                IdentityResult identityResult = await roleManager.DeleteAsync(appRole);

                if (identityResult.Succeeded)
                {
                    AppUserRolesClean(appRole);
                    TempData.Add("Message", $"Role {appRole.Name} have been deleted");

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    AddErrorsFromResult(identityResult);
                }
            }
            else
            {
                ModelState.AddModelError("", "No Role Found");
            }

            return View(nameof(Index), roleManager.Roles);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public void AppUserRolesClean(AppRole appRole)
        {
            foreach (var item in userManager.Users)
            {
                if (appRole.Name == item.RoleName)
                {
                    item.RoleName = null;
                }
            }
        }
    }
}
