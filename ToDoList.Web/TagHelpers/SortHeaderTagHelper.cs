using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ToDoList.Domain.Enums;

namespace ToDoList.Web.TagHelpers;

public class SortHeaderTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    public SortHeaderTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;
    public string? PageAction { get; set; }
    public SortState Property { get; set; }
    public string PropertyName { get; set; } = String.Empty;
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
        TagBuilder tag = new("a");
        tag.Attributes["href"] = urlHelper.Action(PageAction, new {sortOrder = Property});
        tag.InnerHtml.Append(PropertyName);
        output.Content.AppendHtml(tag);
    }
}