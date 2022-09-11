using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeCare_4_0.Controllers
{
    [RedirectingAction]
    public class BaseController : Controller { }

    public class RedirectingActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var tokenAd = filterContext.HttpContext.Request.Cookies["AuthenAD"];

            base.OnActionExecuting(filterContext);
            var path = filterContext.HttpContext.Request.Path;
            var queryString = filterContext.HttpContext.Request.QueryString;
            var counter = 0;
            foreach (string parameterName in queryString)
            {
                counter++;
                var parameterValue = queryString[parameterName];
                if (counter == 1)
                {
                    path += "?" + parameterName + "=" + parameterValue;
                }
                else
                {
                    path += "&" + parameterName + "=" + parameterValue;
                }
            }
            if (HttpContext.Current.Session["userInfo"] == null)
            {
                var routeConfig = new
                {
                    Controller = "HC",
                    Action = "Login",
                    ReturnUrl = path
                };
                var routeValueDictionary = new RouteValueDictionary(routeConfig);
                filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
            }
            else
            {
                if (tokenAd != null)
                {
                    if (System.Security.Claims.ClaimsPrincipal.Current.FindFirst("exp") != null)
                    {
                        var expireOn = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("exp").Value;

                        long unixDate = long.Parse(expireOn);
                        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0);
                        DateTime dateExp = start.AddSeconds(unixDate);

                        var info = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                        DateTime localServerTime = DateTime.Now;
                        DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);
                        DateTimeOffset localTimeExp = TimeZoneInfo.ConvertTime(dateExp, info).ToLocalTime();

                        var remaining = (localTimeExp - localTime.AddHours(-5)).TotalMinutes;

                        if (remaining < 0)
                        {
                            var routeConfig = new
                            {
                                Controller = "HC",
                                Action = "SignOut",
                                ReturnUrl = path
                            };
                            var routeValueDictionary = new RouteValueDictionary(routeConfig);
                            filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
                        }
                    }
                    else {

                        var routeConfig = new
                        {
                            Controller = "HC",
                            Action = "SignOut",
                            ReturnUrl = path
                        };
                        var routeValueDictionary = new RouteValueDictionary(routeConfig);
                        filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
                    }

                   
                }

            }
            
        }
    }
}