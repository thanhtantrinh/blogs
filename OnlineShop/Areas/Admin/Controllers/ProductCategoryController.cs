using AutoMapper;
using Common;
using Model.Dao;
using Model.EF;
using Model.Repository;
using Model.ViewModel;
using OnlineShop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        
        // GET: Admin/ProductCategory
        public ActionResult Index(int? page, int pageSize = 10, string sortBy="")
        {
            //var dao = new ProductCategoryDao();      
            //var model = dao.ListAllPaging(searchString, page, pageSize, catId);
            ProductCategoryFilter filter = new ProductCategoryFilter();
            int totalitem = 0;
            int limitItem = 0;

            Tuple<List<ProductCategoryView>, int> result = _proCategory.getCategories(filter, page??1, pageSize, sortBy);
            if (result.Item2 > 0)
            {
                totalitem = result.Item2;
            }
            ViewBag.TotalItem = totalitem;
            ViewBag.currentPage = page ?? 1;
            ViewBag.limit = limitItem;
            ViewBag.Filter = filter;

            return View(result.Item1);
        }

        [HttpPost]
        public ActionResult Index(ProductCategoryFilter filter, string searchBy = "")
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            //string errorString = "";
            //bool IsValid = true;

            return RedirectToAction("Index");
        }

        // GET: Admin/ProductCategory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/ProductCategory/Create
        public ActionResult Create()
        {
            this.SetViewBag();
            return View();
        }
        // POST: Admin/ProductCategory/Create
        [HttpPost]
        public ActionResult Create(ProductCategoryView model)
        {
            try
            {
                // TODO: Add insert logic here
                var dao = new ProductCategoryDao();
                Mapper.Initialize(cfg => cfg.CreateMap<ProductCategoryView, ProductCategory>());
                var data = Mapper.Map<ProductCategoryView, ProductCategory>(model);
                var result = dao.Insert(data);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/ProductCategory/Edit/5
        public ActionResult Edit(long id)
        {
            var dao = new ProductCategoryDao();
            var model = dao.Find(id);
            this.SetViewBag();
            return View(model);
        }

        // POST: Admin/ProductCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductCategoryView model)
        {
            try
            {
                // TODO: Add update logic here
                var dao = new ProductCategoryDao();
                Mapper.Initialize(cfg => cfg.CreateMap<ProductCategoryView, ProductCategory>());
                var data = Mapper.Map<ProductCategoryView, ProductCategory>(model);
                dao.Update(data);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Admin/ProductCategory/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var dao = new ProductCategoryDao();
                var result = dao.Delete(id);
                string msg = "";
                if (result > 0)
                {
                    msg = "Xóa sản phẩm thành công";
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(new { status = false, Messger = msg });
                }
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new { status = false, Messger = ex.Message });
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Admin/ProductCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var dao = new ProductCategoryDao();
                var result = dao.Delete(id);
                string msg = "";
                if (result > 0)
                {
                    msg = "Xóa sản phẩm thành công";
                }

                if (Request.IsAjaxRequest())
                {
                    return Json(new { status = false, Messger = msg });
                }
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new { status = false, Messger = ex.Message });
                }
            }
            return RedirectToAction("Index");
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            //List< SelectListItem > CatProduct = new List<SelectListItem>();
            ViewBag.ParentID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}
