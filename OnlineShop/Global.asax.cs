using OnlineShop.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OnlineShop.Models;
using OnlineShop.Helpers;
using System.Security.Principal;
using System.Web.Security;
using OnlineShop.Identity;
using System.Threading;

namespace OnlineShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //load config
            SiteConfiguration.StoreSettings();
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

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
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("X-Frame-Options");
            Response.AddHeader("X-Frame-Options", "AllowAll");
        }
    }
}
