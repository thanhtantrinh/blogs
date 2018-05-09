using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.ViewModel;
using OnlineShop.Helpers;
using Common;
namespace OnlineShop.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        [Route("danh-muc-san-pham")]
        public ActionResult Category(int cateId=0, int page = 1, int pageSize = 20)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            //var model = new ProductDao().ProductsPaging("", 1, pageSize, cateId);
            ProductFilter filter = new ProductFilter();
            filter.CategoryId = cateId;
            filter.CatalogueId = SiteConfiguration.CatalogueId;
            filter.Status = new string[] { nameof(StatusEntity.Active) };

            var model = _productRepo.GetProductPaging(filter, page, pageSize);
            return View(model);
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        //[OutputCache(CacheProfile = "Cache1DayForProduct")]
        public ActionResult Detail(long id, long ProductDetailId=0)
        {
            var product = new ProductDao().Find(id, ProductDetailId);
            return View(product);
        }

        [Route("{category}-{cateId}/{metatitle}-{id}")]
        public ActionResult Product(long id, long cateId)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryId);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(id);
            return View(product);
        }

    }
}