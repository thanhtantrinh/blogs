using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        SlideDao dao = new SlideDao();
        // GET: Admin/Slide
        public ActionResult Index()
        {
            var model = dao.ListAll();
            return View(model);
        }

        // GET: Admin/Slide/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Slide/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slide/Create
        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here                    
                    var id = dao.Insert(slide);
                    if (id > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", StaticResources.Resources.InsertCategoryFailed);
                    }
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: Admin/Slide/Edit/5
        public ActionResult Edit(int id)
        {
            var model = dao.FindSlideId(id);
            return View(model);
        }

        // POST: Admin/Slide/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Slide/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Slide/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                new SlideDao().Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SetActive(int id)
        {
            var result = true;
            try
            {
                result = dao.SetActive(id);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, Messger = ex.Message });
            }
            return Json(new { status = result, Messger = "Set Active Slider" });

        }
    }
}
