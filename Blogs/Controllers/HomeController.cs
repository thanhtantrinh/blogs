using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Blogs.Helpers;
using Common;
using System.Text;
using Common.Helpers;

namespace Blogs.Controllers
{
    public class HomeController : BaseController
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
        #region PartialView

        public ActionResult SideBar()
        {
            var model = new List<MenuItem>();
            string message = "";
            model = Helper.GetMenuByCatalogueId(SiteConfiguration.CatalogueId, out message);
            return PartialView("_SidebarNav", model);
        }

        #endregion

        #region For SEO
        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            var sitemap = new SiteMapHelper();
            var sitemapNodes = GetSitemapNodes();
            string xml = sitemap.GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }

        [OutputCache(Duration = 86400)]
        [Route("robots.txt")]
        public ActionResult RobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("user-agent: *");
            stringBuilder.AppendLine("disallow: /Administration/"); //Disallow: /Administration/            Disallow: / Account /
            stringBuilder.AppendLine("disallow: /Account/"); //Disallow: /Administration/            Disallow: / Account /
            stringBuilder.AppendLine("allow: /error/foo");
            stringBuilder.Append("sitemap: ");
            stringBuilder.AppendLine(this.Url.RouteUrl("Sitemap", null, this.Request.Url.Scheme).TrimEnd('/'));
            return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }
        #endregion
    }
}