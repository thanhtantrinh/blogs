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
using Model.EF;

namespace OnlineShop.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {  
            //var productDao = new ProductDao();
            //var contentDao = new ContentDao();
            //ViewBag.NewProducts = productDao.ListNewProduct(4);
            //ViewBag.ListFeatureProducts = productDao.ListFeatureProduct(4);
            //ViewBag.ListNewsEvent = contentDao.ListNewsEvent(4);
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
            //var model = new MenuDao().GetMenuProducts(SiteConfiguration.CatalogueId);
            var model = _proCatRepo.GetMenuCategoryProduct(SiteConfiguration.CatalogueId);
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
            var filter = new ProductFilter();
            filter.CatalogueId = SiteConfiguration.CatalogueId;
            filter.IsShowHome = true;
            filter.Status = new string[] { nameof(StatusEntity.Active) };
            List<v_Product> model = _productRepo.GetProducts(filter, "", 8);
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
            var filter = new ProductFilter();
            filter.CatalogueId = SiteConfiguration.CatalogueId;
            filter.Status = new string[] { nameof(StatusEntity.Active) };
            List<v_Product> model = _productRepo.GetProducts(filter, "ViewCount", 8);            
            return PartialView(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _blockProductNew()
        {
            var filter = new ProductFilter();
            filter.CatalogueId = SiteConfiguration.CatalogueId;
            filter.IsNew = true;
            filter.Status = new string[] { nameof(StatusEntity.Active) };
            
            List<v_Product> model = _productRepo.GetProducts(filter, "", 8);                        
            return PartialView(model);
        }
    }
}