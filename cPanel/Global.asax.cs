﻿using cPanel.Helpers;
using cPanel.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace cPanel
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static SessionStateSection SessionStateSection = (System.Web.Configuration.SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SiteConfiguration.StoreSettings();
        }
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            IPrincipal currentUser = HttpContext.Current.User;
            if (currentUser != null)
            {
                if (currentUser.Identity.IsAuthenticated && currentUser.Identity is FormsIdentity)
                {
                    // Get Forms Identity From Current User
                    FormsIdentity fIdent = (FormsIdentity)HttpContext.Current.User.Identity;

                    // Create a CustomIdentity based on the FormsAuthenticationTicket  
                    CustomIdentity ci = new CustomIdentity(fIdent.Ticket);

                    // Create a custom Principal Instance
                    CustomPrincipal principal = new CustomPrincipal(ci);

                    // Attach the CustomPrincipal to HttpContext.User and Thread.CurrentPrincipal
                    HttpContext.Current.User = Thread.CurrentPrincipal = principal;
                }
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg.Equals("User", StringComparison.InvariantCultureIgnoreCase))
            {
                var cookieName = SessionStateSection.CookieName;
                var cookie = context.Request.Cookies[cookieName];
                return cookie.Value;
            }
            return base.GetVaryByCustomString(context, arg);
        }
    }
}
