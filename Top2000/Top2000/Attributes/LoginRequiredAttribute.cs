using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Top2000.Attributes
{
    /// <summary>
    /// When this action filter gets executed you get sent back to the year list when you are logged in.
    /// </summary>
    public class LoginRequiredAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Make sure you can't access this page when you are logged in
            var principal = filterContext.HttpContext.Session["principal"];
            if (principal == null)
            {
                // Redirect back to the song list
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "YearList" },
                        { "action", "Index" }
                });
            }
        }
    }
}