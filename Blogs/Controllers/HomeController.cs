using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Blogs.Helpers;

namespace Blogs.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("gioi-thieu")]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult SideBar()
        {
            var model = new List<MenuItem>();
            string message = "";
            
            model = Helper.GetMenuByCatalogueId(SiteConfiguration.CatalogueId, out message);

            //if (!String.IsNullOrWhiteSpace(message))
            //{

            //}     
            
            return PartialView("_SidebarNav", model);
        }
    }
}