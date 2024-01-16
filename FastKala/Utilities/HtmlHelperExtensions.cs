using Microsoft.AspNetCore.Mvc.Rendering;

namespace FastKala.Utilities;

public static class HtmlHelperExtensions
{
    public static string ActiveClass(this IHtmlHelper htmlHelper, string pageRoute)
    {
        var routeData = htmlHelper.ViewContext.RouteData;

        var page = routeData.Values["page"]?.ToString();

        return pageRoute == page ? "" : "collapsed";
    }

    public static string ActiveClass(this IHtmlHelper htmlHelper, string? controllers = null, string? actions = null, string cssClassActive = "active", string cssClassNotActive = "")
    {
        var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
        var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;

        var acceptedControllers = (controllers ?? currentController ?? "").Split(',');
        var acceptedActions = (actions ?? currentAction ?? "").Split(',');

        return acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
            ? cssClassActive
            : cssClassNotActive;
    }
}