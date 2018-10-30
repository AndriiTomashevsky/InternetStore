using InternetStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace InternetStore.Infrastructure.TagHelpers
{
    [HtmlTargetElement("nav", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        public PagingInfo PageModel { get; set; }
        public string PagePrevious { get; set; } = "Previous";
        public string PageNext { get; set; } = "Next";
        public bool PageClassesEnabled { get; set; } = false;
        public bool PageAriasEnabled { get; set; } = false;
        public string PageAction { get; set; }
        public string ClassUnorderedList { get; set; }
        public string PageClassListItem { get; set; }
        public string PageClassAnchor { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

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
            PageUrlValues["page"] = PageModel.CurrentPage;

            output.TagName = "nav";
            output.PreContent.SetHtmlContent($"<ul class=\"{ClassUnorderedList}\">");


            if (PageModel.HasPreviousPage)
            {
                TagBuilder previous = CreatePreTag(PageModel.CurrentPage - 1, urlHelper);
                output.PreContent.AppendHtml(previous);

                if (PageModel.HasPreviousPage && PageModel.CurrentPage != 2)
                {
                    TagBuilder prevPrevItem = CreateTag(PageModel.CurrentPage - 2, urlHelper);
                    output.PreContent.AppendHtml(prevPrevItem);
                }

                TagBuilder prevItem = CreateTag(PageModel.CurrentPage - 1, urlHelper);
                output.PreContent.AppendHtml(prevItem);
            }
            else
            {
                TagBuilder previous = CreatePreTag(PageModel.CurrentPage - 1, urlHelper);
                output.PreContent.AppendHtml(previous);
            }

            TagBuilder currentItem = CreateTag(PageModel.CurrentPage, urlHelper);
            output.Content.SetHtmlContent(currentItem);

            if (PageModel.HasNextPage)
            {
                TagBuilder next = CreatePostTag(PageModel.CurrentPage + 1, urlHelper);
                output.PostContent.SetHtmlContent(next);

                TagBuilder nextItem = CreateTag(PageModel.CurrentPage + 1, urlHelper);
                output.Content.AppendHtml(nextItem);

                if (PageModel.HasNextPage && PageModel.CurrentPage + 1 != PageModel.TotalPages)
                {
                    TagBuilder nextNextItem = CreateTag(PageModel.CurrentPage + 2, urlHelper);
                    output.Content.AppendHtml(nextNextItem);
                }
            }
            else
            {
                TagBuilder next = CreatePostTag(PageModel.CurrentPage + 1, urlHelper);
                output.PostContent.AppendHtml(next);
            }

            output.PostContent.AppendHtml("</ul>");
        }

        TagBuilder CreateTag(int currentPage, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");

            if (currentPage == PageModel.CurrentPage)
            {
                item.AddCssClass("active");
            }
            else
            {
                PageUrlValues["page"] = currentPage;
                link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }
            link.InnerHtml.Append(currentPage.ToString());
            item.InnerHtml.AppendHtml(link);

            return item;
        }

        TagBuilder CreatePreTag(int currentPage, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            TagBuilder span = new TagBuilder("span");

            if (PageAriasEnabled)
            {
                span.Attributes["aria-hidden"] = "true";
                link.Attributes["aria-label"] = "Previous";
            }

            if (PageModel.HasPreviousPage)
            {
                PageUrlValues["page"] = currentPage;
                link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }
            else
            {
                item.AddCssClass("disabled");
            }

            span.InnerHtml.AppendHtml("&laquo;");

            link.InnerHtml.AppendHtml(span);
            item.InnerHtml.AppendHtml(link);

            return item;
        }

        TagBuilder CreatePostTag(int currentPage, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            TagBuilder span = new TagBuilder("span");

            if (PageAriasEnabled)
            {
                span.Attributes["aria-hidden"] = "true";
                link.Attributes["aria-label"] = "Next";
            }
            if (PageModel.HasNextPage)
            {
                PageUrlValues["page"] = currentPage;
                link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }
            else
            {
                item.AddCssClass("disabled");
            }

            span.InnerHtml.AppendHtml(PageNext);

            link.InnerHtml.AppendHtml(span);
            item.InnerHtml.AppendHtml(link);

            return item;
        }
    }
}
