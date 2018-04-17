using Model.Dao;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Model.ViewModel;
using Common;
using OnlineShop.Helpers;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {  
            var productDao = new ProductDao();
            var contentDao = new ContentDao();
            ViewBag.NewProducts = productDao.ListNewProduct(4);
            ViewBag.ListFeatureProducts = productDao.ListFeatureProduct(4);
            ViewBag.ListNewsEvent = contentDao.ListNewsEvent(4);
            return View();
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult Right()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult _BlockCategoryProduct(long CatId)
        {
            CategoryBlogViewModel model = new CategoryDao().CategoryBlog(CatId);
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {
            //var model = new FooterDao().GetFooter();
            return PartialView();
        }

        //[ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _Header()
        {
            return PartialView();
        }

        public ActionResult _MenuCatProduct()
        {
            var model = new MenuDao().GetMenuProducts(SiteConfiguration.CatalogueId);
            return PartialView(model);
        }

        public ActionResult _Sliders()
        {
            var model = new SlideDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _MenuLeft()
        {
            return PartialView();
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _Logo()
        {
            return PartialView();
        }
        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _MenuTop()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);            
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _blockProductHome()
        {
            var model = new ProductDao().ProductsPaging(null, 1, 8, 0, true, true);
            return PartialView(model);
        }


        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _blockProducts(long catId=0)
        {
            var model = new ProductDao().ProductsPaging("", 1, 10, catId, true);
            return PartialView(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _blockProductFeatures()
        {
            var model = new ProductDao().ProductsPaging("", 1, 8, 0, true);
            return PartialView(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _blockProductNew()
        {
            var model = new ProductDao().ProductsPaging("", 1, 8, 0, false,true);
            return PartialView(model);
        }
    }
}