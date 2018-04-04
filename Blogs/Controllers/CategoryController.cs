using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.ViewModel;
using Model.EF;
using System.Net;
using StaticResources;

namespace Blogs.Controllers
{
    [RoutePrefix("danh-muc")]
    [Route("{action=index}")]
    public class CategoryController : BaseController
    {
        public CategoryController() : base()
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            return View();
        }
                
        [HttpGet]
        public ActionResult Detail(int Id=0, int page = 1, int pageSize = 10)
        {
            IEnumerable<v_Content> model;
            ContentFilter filter = (ContentFilter)Session["ContentFilter"];
            if (filter == null)
            {
                filter = new ContentFilter();
                filter.Status = "1";
                Session["ContentFilter"] = filter;
            }

            if (Id>0)
            {
                filter.CategoryID = Id;
                model = _contentDAO.ListAllPaging(filter, page, pageSize);
                if (model != null && model.Count() > 0)
                {
                    ViewBag.Title = model.FirstOrDefault().CategoryName;
                }
                else
                {
                    var categoryView = _categoryDAO.GetByID(Id);
                    if (categoryView!=null)
                    {
                        ViewBag.Title = categoryView.Name;
                    }
                    else
                    {
                        ViewBag.Title = Resources.MSG_THE_CATEGORY_HAS_NOT_FOUND;
                    }                    
                }
                return View(model);
            }            
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpGet]
        public ActionResult ContentDetail(int Id = 0)
        {
            var model = new ContentViewModel();
            if (Id>0)
            {
                model = _contentDAO.GetByID(Id);
                if (model!=null)
                {
                    ViewBag.Title = model.Name;
                    return View(model);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
    }
}