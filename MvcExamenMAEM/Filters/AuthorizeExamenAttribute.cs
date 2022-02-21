using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2MAEM.Filters
{
    public class AuthorizeExamenAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            string id = "";

            if (context.RouteData.Values["id"] != null)
            {

                id = context.RouteData.Values["id"].ToString();
            }

            ITempDataProvider provider = context.HttpContext.RequestServices.GetService(typeof(ITempDataProvider)) as ITempDataProvider;

            var TempData = provider.LoadTempData(context.HttpContext);

            TempData["controller"] = controller;
            TempData["action"] = action;
            TempData["id"] = id;

            provider.SaveTempData(context.HttpContext, TempData);


            if (user.Identity.IsAuthenticated == false)
            {

                context.Result = this.GetRuta("Manage", "Login");
            }
        }

        private RedirectToRouteResult GetRuta(string controller, string action)
        {

            RouteValueDictionary data = new RouteValueDictionary(new
            {

                controller = controller,
                action = action
            });

            RedirectToRouteResult routa = new RedirectToRouteResult(data);

            return routa;
        }
    }
}
