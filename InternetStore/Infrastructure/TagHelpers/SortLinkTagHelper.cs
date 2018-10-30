using InternetStore.Models;
using InternetStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InternetStore.Infrastructure.TagHelpers
{
    //[HtmlTargetElement("sortlink")]
    public class SortLinkTagHelper : TagHelper
    {
        IUrlHelperFactory urlHelperFactory;

        public ProductsListViewModel ViewModel { get; set; }
        public SortState Property { get; set; }
        public bool Asc { get; set; }
        public string Sort { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public SortLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder anchor = new TagBuilder("a");
            TagBuilder span = new TagBuilder("span");
            string append = null;

            anchor.AddCssClass("btn btn-default pull-right");
            anchor.Attributes["href"] = urlHelper.Action("List", new { sortState = Property});

            if (Property == SortState.NameAsc || Property == SortState.NameDesc)
            {
                span.AddCssClass(Property == SortState.NameAsc ? "glyphicon glyphicon-sort-by-alphabet" :
                    "glyphicon glyphicon-sort-by-alphabet-alt");
                append = " Sort by Name";
            }

            if (Property == SortState.PriceAsc || Property == SortState.PriceDesc)
            {
                span.AddCssClass(Property == SortState.PriceAsc ? "glyphicon glyphicon-sort-by-order" :
                    "glyphicon glyphicon-sort-by-order-alt");
                append = " Sort by Price";
            }

            anchor.InnerHtml.AppendHtml(span);
            anchor.InnerHtml.Append(append);

            output.TagName = "";
            output.Content.AppendHtml(anchor);
        }
    }
}
