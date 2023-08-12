using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ToDoList.Domain.Entities;

namespace ToDoList.Web.TagHelpers;

public class PageLinkTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext] [HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = null!;

    public PagingInfo? PagingInfo { get; set; }
    public string? PageAction { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (PagingInfo != null)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new("div");
            for (var i = 1; i <= PagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { taskPage = i });
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}