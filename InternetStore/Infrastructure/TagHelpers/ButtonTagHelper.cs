using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InternetStore.Infrastructure.TagHelpers
{
    [HtmlTargetElement("button", Attributes = "bs-button-color")]
    public class ButtonTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string currentController = (string)ViewContext.RouteData.Values["controller"];
            HtmlString htmlString = (HtmlString)output.Attributes["class"].Value;
            string value = htmlString.Value;

            if (currentController == "RoleAdmin")
            {
                value = value + " bg-role";
                output.Attributes.SetAttribute("class", value);
            }

            if (currentController == "Home")
            {
                value = value + " btn-primary";
                output.Attributes.SetAttribute("class", value);
            }
            if (currentController == "Order")
            {
                value = value + " btn-info";
                output.Attributes.SetAttribute("class", value);
            }
            if (currentController == "Category")
            {
                value = value + " btn-success";
                output.Attributes.SetAttribute("class", value);
            }
            if (currentController == "Admin")
            {
                value = value + " btn-warning";
                output.Attributes.SetAttribute("class", value);
            }
        }

    }
}
