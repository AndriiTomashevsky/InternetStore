using System.Collections.Generic;
using System.Threading.Tasks;
using InternetStore.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InternetStore.Infrastructure.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "role-id")]
    public class RoleUsersTagHelper : TagHelper
    {
        UserManager<AppUser> userManager;
        RoleManager<AppRole> roleManager;

        public RoleUsersTagHelper(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public string RoleId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> list = new List<string>();
            AppRole appRole = await roleManager.FindByIdAsync(RoleId);

            if (appRole != null)
            {
                foreach (var item in userManager.Users)
                {
                    if (item != null && await userManager.IsInRoleAsync(item,appRole.Name))
                    {
                        list.Add(item.UserName);
                    }
                }
            }

            output.Content.SetContent(list.Count == 0 ? "No Users" : string.Join(", ", list));
        }
    }
}
