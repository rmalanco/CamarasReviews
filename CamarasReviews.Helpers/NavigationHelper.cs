using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Helpers
{
    public static class NavigationHelper
    {
        public static bool IsNavActive(string area, string controller, string action, ViewContext viewContext)
        {
            var currentArea = viewContext.RouteData.Values["Area"]?.ToString();
            var currentController = viewContext.RouteData.Values["Controller"]?.ToString();
            var currentAction = viewContext.RouteData.Values["Action"]?.ToString();

            return string.Equals(currentArea, area, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsExpandedActive(string area, string controller, string action, ViewContext viewContext)
        {
            var currentArea = viewContext.RouteData.Values["Area"]?.ToString();
            var currentController = viewContext.RouteData.Values["Controller"]?.ToString();
            var currentAction = viewContext.RouteData.Values["Action"]?.ToString();

            return string.Equals(currentArea, area, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase);
        }
    }
}