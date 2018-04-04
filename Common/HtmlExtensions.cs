﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class HtmlExtensions
    {

        public static string AbsoluteAction(this UrlHelper url, string actionName)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
            return url.Action(actionName, null, (RouteValueDictionary)null,
                              requestUrl.Scheme, null);
        }

        public static string AbsoluteAction(this UrlHelper url, string actionName,
                                            object routeValues)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
            return url.Action(actionName, null, new RouteValueDictionary(routeValues),
                              requestUrl.Scheme, null);
        }

        public static string AbsoluteAction(this UrlHelper url, string actionName,
                                            RouteValueDictionary routeValues)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
            return url.Action(actionName, null, routeValues, requestUrl.Scheme, null);
        }

        public static string AbsoluteAction(this UrlHelper url, string actionName,
                                            string controllerName)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
            return url.Action(actionName, controllerName, (RouteValueDictionary)null,
                              requestUrl.Scheme, null);
        }

        public static string AbsoluteAction(this UrlHelper url, string actionName,
                                            string controllerName,
                                            object routeValues)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
            return url.Action(actionName, controllerName,
                              new RouteValueDictionary(routeValues), requestUrl.Scheme,
                              null);
        }

        public static string AbsoluteAction(this UrlHelper url, string actionName,
                                            string controllerName,
                                            RouteValueDictionary routeValues)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
            return url.Action(actionName, controllerName, routeValues, requestUrl.Scheme,
                              null);
        }

        public static string AbsoluteAction(this UrlHelper url, string actionName,
                                            string controllerName, object routeValues,
                                            string protocol)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;
            return url.Action(actionName, controllerName,
                              new RouteValueDictionary(routeValues), protocol, null);
        }

    }
}