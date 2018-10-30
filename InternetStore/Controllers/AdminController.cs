using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetStore.Models.Identity;
using InternetStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetStore.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminController : Controller
    {
        RoleManager<AppRole> roleManager;
        UserManager<AppUser> userManager;
        IPasswordHasher<AppUser> passwordHasher;
        IUserValidator<AppUser> userValidator;
        IPasswordValidator<AppUser> passwordValidator;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            IPasswordValidator<AppUser> passwordValidator,
             IUserValidator<AppUser> userValidator,
              IPasswordHasher<AppUser> passwordHasher)
        {
            this.userManager = userManager;
            this.passwordValidator = passwordValidator;
            this.userValidator = userValidator;
            this.passwordHasher = passwordHasher;
            this.roleManager = roleManager;
        }

        public ViewResult Index()
        {
            ViewBag.IsAdmin = true;

            return View(userManager.Users);

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser { UserName = createModel.Name, Email = createModel.Email, RoleName = createModel.RoleName };

                IdentityResult identityResultUser = await userManager.CreateAsync(appUser, createModel.Password);

                if (createModel.RoleName != null)
                {
                    IdentityResult identityResultRole = await userManager.AddToRoleAsync(appUser, createModel.RoleName);

                    if (!identityResultRole.Succeeded)
                    {
                        AddErrorsFormResult(identityResultRole);
                    }
                }

                if (identityResultUser.Succeeded)
                {
                    TempData.Add("Message", $"User {createModel.Name} has been added");

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    AddErrorsFormResult(identityResultUser);

                    return View(nameof(Index), userManager.Users);
                }
            }
            else
            {
                return View(nameof(Index), userManager.Users);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(string userId, string newEmail, string password, string roleName)
        {
            AppUser appUser = await userManager.FindByIdAsync(userId);
            AppRole appRole = await roleManager.FindByNameAsync(roleName);

            if (appUser != null)
            {
                if (newEmail != null)
                {
                    appUser.Email = newEmail;
                }
                if (appRole != null)
                {
                    await userManager.AddToRoleAsync(appUser, appRole.Name);
                    appUser.RoleName = appRole.Name;
                }

                IdentityResult identityResultEmail = await userValidator.ValidateAsync(userManager, appUser);

                if (identityResultEmail.Succeeded)
                {
                    //There is nothing to do, the email has been changed early
                }
                else
                {
                    AddErrorsFormResult(identityResultEmail);
                }

                IdentityResult identityResultPassword = null;

                if (string.IsNullOrEmpty(password))
                {
                    //There is nothing to do, the password will be left previous
                }
                else
                {
                    identityResultPassword = await passwordValidator.ValidateAsync(userManager, appUser, password);

                    if (identityResultPassword.Succeeded)
                    {
                        appUser.PasswordHash = passwordHasher.HashPassword(appUser, password);
                    }
                    else
                    {
                        AddErrorsFormResult(identityResultPassword);
                    }

                }

                if ((identityResultEmail.Succeeded && identityResultPassword == null)
                    || (identityResultEmail.Succeeded && identityResultPassword.Succeeded && password != string.Empty))
                {
                    IdentityResult identityResult = await userManager.UpdateAsync(appUser);

                    if (identityResult.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        AddErrorsFormResult(identityResult);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(nameof(Index), userManager.Users);
        }

        public async Task<IActionResult> Delete(string userId)
        {
            AppUser user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    AddErrorsFormResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(nameof(Index), userManager.Users);
        }

        private void AddErrorsFormResult(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }


        public async Task<JsonResult> Validate(string name, string email, string newemail, string password)
        {
            if (newemail != null)
            {
                email = newemail;
            }

            AppUser appUser = new AppUser { UserName = name ?? "ValidName", Email = email ?? "ValidEmail@example.com" };

            IdentityResult identityResultAppUser = await userValidator.ValidateAsync(userManager, appUser);
            IdentityResult identityResultPassword = await passwordValidator.ValidateAsync(userManager, appUser, password ?? "validPassword1$");


            if (identityResultAppUser.Succeeded && identityResultPassword.Succeeded)
            {
                return Json(true);
            }

            string description = null;

            if (identityResultAppUser.Errors.Count() != 0)
            {
                foreach (var item in identityResultAppUser.Errors)
                {
                    if (item.Description != null)
                    {
                        description = item.Description;
                    }
                }

            }

            if (identityResultPassword.Errors.Count() != 0)
            {
                foreach (var item in identityResultPassword.Errors)
                {
                    if (item.Description != null)
                    {
                        description = item.Description;
                    }
                }
            }

            return Json(description);
        }
    }
}