using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Top2000.Helpers
{
    public static class AuthHelper
    {
        /// <summary>
        /// Checks if the user is logged in(can be used from the view)
        /// </summary>
        /// <param name="page"></param>
        /// <returns>If the user is logged in</returns>
        public static bool IsLoggedIn(this WebPageRenderingBase page)
        {
            var principal = page.Session["principal"];
            return principal != null;
        }

        /// <summary>
        /// Checks if the user is logged in(can be used from the controller)
        /// </summary>
        /// <param name="page"></param>
        /// <returns>If the user is logged in</returns>
        public static bool IsLoggedIn(this Controller page)
        {
            var principal = page.Session["principal"];
            return principal != null;
        }

        /// <summary>
        /// Retrieves the user from the session(can be used from the view)
        /// </summary>
        /// <param name="page"></param>
        /// <returns>The user from the session or null if not found</returns>
        public static IPrincipal GetUser(this WebPageRenderingBase page)
        {
            // Make sure there is a user available
            if(page.IsLoggedIn())
            {
                // Get the user from the principal
                return page.Session["principal"] as IPrincipal;
            }
            return null;
        }

        /// <summary>
        /// Retrieves the user from the session(can be used from the controller)
        /// </summary>
        /// <param name="page"></param>
        /// <returns>The user from the session or null if not found</returns>
        public static IPrincipal GetUser(this Controller page)
        {
            // Make sure there is a user available
            if (page.IsLoggedIn())
            {
                // Get the user from the principal
                return page.Session["principal"] as IPrincipal;
            }
            return null;
        }
    }
}