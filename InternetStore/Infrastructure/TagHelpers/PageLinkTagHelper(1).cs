using InternetStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InternetStore.Infrastructure.TagHelpers
{
    [HtmlTargetElement("pagelink")]   // List.cshmtl
    public class PageLinkTagHelper : TagHelper
    {
        public PagingInfo Page { get; set; }

        IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

                for (int i = 1; i < Page.TotalPages + 1; i++)
                {
                    TagBuilder listItem = new TagBuilder("li");
                    if (i == Page.CurrentPage)
                    {
                        listItem.AddCssClass("active");
                    }

                    TagBuilder anchor = new TagBuilder("a");
                    anchor.Attributes["href"] = urlHelper.Action("List", new {  Page = i });
                    anchor.InnerHtml.Append(i.ToString());
                    listItem.InnerHtml.AppendHtml(anchor);
                    output.Content.AppendHtml(listItem);
                }
            output.TagName = "";
        }
    }
}
