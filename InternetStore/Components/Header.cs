using InternetStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetStore.Components
{
    public class Header : ViewComponent
    {
        string logo;
        string text;
        string headerClass;

        public IViewComponentResult Invoke(string headerClass)
        {
            string currentController = (string)RouteData.Values["controller"];

            if (currentController == "RoleAdmin")
            {
                logo = "fas fa-address-book";
                text = "Roles";
                this.headerClass = headerClass + " bg-role";
            }
            if (currentController == "Product")
            {
                logo = "fas fa-tags";
                text = "Products";
                this.headerClass = headerClass + " bg-primary";
            }
            if (currentController == "Order")
            {
                logo = "fas fa-cart-arrow-down";
                text = "Orders";
                this.headerClass = headerClass + " bg-info";
            }
            if (currentController == "Category")
            {
                logo = "fas fa-folder-open";
                text = "Categories";
                this.headerClass = headerClass + " bg-success";
            }
            if (currentController == "Admin")
            {
                logo = "fas fa-users";
                text = "Users";
                this.headerClass = headerClass + " bg-warning";
            }

            return View(new HeaderViewModel() { Logo = logo, Text = text, HeaderClass = this.headerClass });
        }
    }
}
