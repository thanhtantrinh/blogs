using Common;
using cPanel.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ViewModel;

namespace cPanel.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class EcommerceController : BaseController
    {
        // GET: Ecommerce
        public ActionResult Index()
        {
            return View();
        }
        #region Category of product manager
        [HttpGet]
        public ActionResult ProductCategory(int page = 1, int pageSize = 20, string sortby = "")
        {
            ProductCategoryFilter filter = (ProductCategoryFilter)Session["ProductCategoryFilter"];
            if (filter == null)
            {
                filter = new ProductCategoryFilter();
                Session["ProductCategoryFilter"] = filter;
            }
            var model = _proRepo.GetCategoriesPaging(filter, page, pageSize, sortby);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách danh mục";
            return View(model);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCategory(ProductCategoryFilter filter)
        {
            if (filter != null)
                Session["ProductCategoryFilter"] = filter;
            else
                Session["ProductCategoryFilter"] = new ProductCategoryFilter();

            return RedirectToAction("CategoryProduct");            
        }

        public ActionResult ProductCategoryEdit()
        {

            return View();
        }

        public ActionResult ProductCategoryCreate()
        {

            return View();
        }
        #endregion

        #region Product manager
        public ActionResult Product()
        {

            return View();
        }
        public ActionResult ProductEdit()
        {

            return View();
        }

        public ActionResult ProductCreate()
        {

            return View();
        }
        #endregion
    }
}