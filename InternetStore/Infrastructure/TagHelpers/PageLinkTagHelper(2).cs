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
        public SortingInfo Sorting { get; set; }

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
            output.TagName = "";

            if (Page.HasPreviousPage)
            {
                TagBuilder previous = CreateTag(Page.CurrentPage - 1, urlHelper, IsPrevious: true);
                output.Content.AppendHtml(previous);

                TagBuilder prevItem = CreateTag(Page.CurrentPage - 1, urlHelper);
                output.Content.AppendHtml(prevItem);
                if (Page.HasPreviousPage && Page.CurrentPage != 2)
                {
                    TagBuilder prevPrevItem = CreateTag(Page.CurrentPage - 2, urlHelper);
                    output.PreContent.AppendHtml(prevPrevItem);
                }
            }
            else
            {
                TagBuilder previous = CreateTag(Page.CurrentPage - 1, urlHelper, IsPrevious: true);

                //TagBuilder listItem = new TagBuilder("li");
                //TagBuilder anchor = new TagBuilder("a");
                //TagBuilder span = new TagBuilder("span");

                //listItem.AddCssClass("disabled");
                //anchor.Attributes["aria-label"] = "Previous";
                //span.Attributes["aria-hidden"] = "true";
                //span.InnerHtml.AppendHtml("&laquo;");
                //anchor.InnerHtml.AppendHtml(span);
                //listItem.InnerHtml.AppendHtml(anchor);
                output.PreContent.AppendHtml(previous);
            }

            TagBuilder currentItem = CreateTag(Page.CurrentPage, urlHelper);
            output.Content.AppendHtml(currentItem);

            if (Page.HasNextPage)
            {
                TagBuilder nextItem = CreateTag(Page.CurrentPage + 1, urlHelper);
                output.Content.AppendHtml(nextItem);

                if (Page.HasNextPage && Page.CurrentPage + 1 != Page.TotalPages)
                {
                    TagBuilder nextNextItem = CreateTag(Page.CurrentPage + 2, urlHelper);
                    output.Content.AppendHtml(nextNextItem);
                }
            }
            output.TagName = "";
        }
        TagBuilder CreateTag(int currentPage, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            TagBuilder span = new TagBuilder("span");

            span.Attributes["aria-hidden"] = "true";

            if (currentPage == this.Page.CurrentPage && !IsNext && !IsPrevious)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action("List", new { page = currentPage, sortState = Sorting.CurrentSortState, search = Page.Search });
            }

            if (IsPrevious || IsNext)
            {
                item.AddCssClass("disabled");
            }

            if (IsPrevious)
            {
                span.InnerHtml.AppendHtml("&laquo;");
                link.Attributes["aria-label"] = "Previous";
                link.InnerHtml.AppendHtml(span);
            }

            if (IsNext)
            {
                span.InnerHtml.AppendHtml("&raquo;");
                link.Attributes["aria-label"] = "Next";
                link.InnerHtml.AppendHtml(span);
            }

            if (!IsNext && !IsPrevious)
            {
                link.InnerHtml.Append(currentPage.ToString());
            }

            item.InnerHtml.AppendHtml(link);

            return item;
        }

        TagBuilder CreateRreviousOrNextTag(int currentPage, IUrlHelper urlHelper, bool IsPrevious = false, bool IsNext = false)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            TagBuilder span = new TagBuilder("span");

            span.Attributes["aria-hidden"] = "true";

            if (currentPage == this.Page.CurrentPage && !IsNext && !IsPrevious)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action("List", new { page = currentPage, sortState = Sorting.CurrentSortState, search = Page.Search });
            }

            if (IsPrevious || IsNext)
            {
                item.AddCssClass("disabled");
            }

            if (IsPrevious)
            {
                span.InnerHtml.AppendHtml("&laquo;");
                link.Attributes["aria-label"] = "Previous";
                link.InnerHtml.AppendHtml(span);
            }

            if (IsNext)
            {
                span.InnerHtml.AppendHtml("&raquo;");
                link.Attributes["aria-label"] = "Next";
                link.InnerHtml.AppendHtml(span);
            }

            if (!IsNext && !IsPrevious)
            {
                link.InnerHtml.Append(currentPage.ToString());
            }

            item.InnerHtml.AppendHtml(link);

            return item;
        }

    }
}
