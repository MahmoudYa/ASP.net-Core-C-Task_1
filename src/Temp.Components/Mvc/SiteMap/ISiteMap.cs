using Microsoft.AspNetCore.Mvc.Rendering;

namespace Temp.Components.Mvc;

public interface ISiteMap
{
    SiteMapNode[] For(ViewContext context);
    SiteMapNode[] BreadcrumbFor(ViewContext context);
}
