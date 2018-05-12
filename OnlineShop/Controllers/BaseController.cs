using Model.Dao;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Model.Repository;
using OnlineShop.Helpers;

namespace OnlineShop.Controllers
{
    public class BaseController : Controller
    {
        public ContentDao _contentDAO = new ContentDao();
        public CategoryDao _categoryDAO = new CategoryDao();
        //public BaseRepository baseRepository = new BaseRepository();
        public OrderRepo _orderRepo = new OrderRepo();
        public ProductRepo _productRepo = new ProductRepo();

        ////initilizing culture on controller initialization
        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    base.Initialize(requestContext);
        //    if (Session[CommonConstants.CULTURE_SESSION] != null)
        //    {
        //        Thread.CurrentThread.CurrentCulture = new CultureInfo(CommonConstants.CULTURE_SESSION.ToString());
        //        Thread.CurrentThread.CurrentUICulture = new CultureInfo(CommonConstants.CULTURE_SESSION.ToString());
        //    }
        //}
        //// changing culture

        //public ActionResult ChangeCulture(string currentCulture, string returnUrl)
        //{
        //    Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentCulture);

        //    Session[CommonConstants.CULTURE_SESSION] = currentCulture;
        //    return Redirect(returnUrl);
        //}
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        } 
    }
}