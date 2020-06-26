using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Top2000.Helpers
{
    public static class PageHelper
    {
        /// <summary>
        /// Checks if the current page is active in the navigation bar and returns in active class
        /// Can be used as @Html.IsSelected
        /// </summary>
        /// <param name="html"></param>
        /// <param name="controllers"></param>
        /// <param name="actions"></param>
        /// <param name="cssClass"></param>
        /// <returns>The active css class if the action is active</returns>
        public static string IsSelected(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "nav-active")
        {
            ViewContext viewContext = html.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            RouteValueDictionary routeValues = viewContext.RouteData.Values;
         
            // Get the current action and controller
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            // Make sure the actions are not empty
            if (String.IsNullOrEmpty(actions))
                actions = currentAction;

            // Make sure the controllers are not empty
            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            // Split the actions and controllers
            string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            // Check if the current action is the active action
            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }
    }
}