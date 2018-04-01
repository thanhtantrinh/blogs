using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Model.EF;
using Model.Repository;
using cPanel.Helpers;
using cPanel.Identity;
using Common;

namespace cPanel.Controllers
{
    public class BaseController : Controller
    {
        //protected OnlineShopEntities db = new OnlineShopEntities();        
        protected BaseRepository _baseRepository = new BaseRepository(SiteConfiguration.DbConnectionString);
        protected CatalogueRepo _catalogueRepo = new CatalogueRepo();
        protected ProductRepo _proRepo = new ProductRepo();
        protected ContentRepo _contentRepo = new ContentRepo();

        public CustomIdentity CurrentUser;

        //initilizing culture on controller initialization
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            CustomIdentity ident = User.Identity as CustomIdentity;
            if (ident != null)
            {
                CurrentUser = ident;
            }

            if (Session[CommonConstants.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
            }
            else
            {
                Session[CommonConstants.CurrentCulture] = "vi";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi");
            }
        }

        // changing culture
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);
            Session[CommonConstants.CurrentCulture] = ddlCulture;
            return Redirect(returnUrl);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        } 
        
    }
}