using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            var db = new MenuDao();
            var model = db.ListByGroupId(2);
            return View(model);
        }

        // GET: Admin/Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/Menu/Create
        [HttpPost]
        public ActionResult Create(Menu model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var dao = new MenuDao();
                    var result = dao.Create(model);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int id)
        {
            var db = new MenuDao();
            var model = db.GetByID(id);
            return View(model);
        }

        // POST: Admin/Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(Menu model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var dao = new MenuDao();
                    var result = dao.Update(model);
                    return RedirectToAction("Index");
                }
                
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
