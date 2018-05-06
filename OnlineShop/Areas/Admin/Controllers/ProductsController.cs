using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.Dao;
using Model.ViewModel;
using System.IO;
using System.Web.Script.Serialization;
using Common;
using OnlineShop.Helpers;
using OnlineShop.Resource;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString = "", long catId = 0)
        {
            var dao = new ProductDao();
            var catDAO = new ProductCategoryDao();
            ViewBag.SearchString = searchString;
            ViewBag.catId = catId;

            ViewBag.CategoryID = new SelectList(catDAO.ListAll(), "ID", "Name", catId);

            var model = dao.ProductsPaging("", 1, 10, catId);
            return View(model);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int id)
        {
            var DAO = new ProductDao();
            var model = DAO.Find(id);

            return View(model);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            var catDAO = new ProductCategoryDao();
            SetViewBag();
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductsView product)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                var DAO = new ProductDao();
                var result = DAO.Add(product);
                if (result.Id>0)
                {
                    if (product.Images != null && !String.IsNullOrWhiteSpace(product.Images.FileName))
                    {
                        string path = Path.Combine(Server.MapPath("~/Images/Products/large"), Path.GetFileName(result.Id.ToString() + "_" + product.Images.FileName));
                        product.Images.SaveAs(path);
                        //resize cho vao tung thu muc
                        ImageHelper.CreateThumbnail(150, 150, path, Server.MapPath("~/Images/Products/small/"));
                        ImageHelper.CreateThumbnail(80, 80, path, Server.MapPath("~/Images/Products/min/"));
                        ImageHelper.CreateThumbnail(500, 500, path, Server.MapPath("~/Images/Products/medium/"));
                    }
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, SiteResource.MSG_THE_PRODUCT_HAS_BEEN_CREATED);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;
                    return RedirectToAction("Index");
                }
                else
                {
                    actionStatus.ActionStatus = ResultSubmit.failed;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, SiteResource.MSG_THE_PRODUCT_HAS_NOT_BEEN_CREATED);
                }

                Session["ACTION_STATUS"] = actionStatus;
                return RedirectToAction("Index");
            }
            else
            {
                IsValid = false;
                foreach (var item in ModelState.Values)
                {
                    if (item.Errors.Count() > 0)
                    {
                        var errorItems = item.Errors.Where(f => !String.IsNullOrWhiteSpace(f.ErrorMessage)).ToList();
                        foreach (var erroritem in errorItems)
                        {
                            errorString += "<br />" + erroritem.ErrorMessage;
                        }
                    }
                }
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_WARNING, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var DAO = new ProductDao();
            var model = DAO.Find(id);           
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductsView product)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;
            if (ModelState.IsValid)
            {
                var DAO = new ProductDao();

                if (product.Images != null && !String.IsNullOrWhiteSpace(product.Images.FileName))
                {
                    string path = Path.Combine(Server.MapPath("~/Images/Products/large"), Path.GetFileName(product.ID.ToString() + "_" + product.Images.FileName));
                    product.Images.SaveAs(path);
                    //resize cho vao tung thu muc
                    ImageHelper.CreateThumbnail(150, 150, path, Server.MapPath("~/Images/Products/small/"));
                    ImageHelper.CreateThumbnail(80, 80, path, Server.MapPath("~/Images/Products/min/"));
                    ImageHelper.CreateThumbnail(500, 500, path, Server.MapPath("~/Images/Products/medium/"));
                }
                var result = DAO.Update(product);
                if (result)
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, SiteResource.MSG_THE_PRODUCT_HAS_BEEN_UPDATED);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;
                    return RedirectToAction("Index");
                }
                else
                {
                    actionStatus.ActionStatus = ResultSubmit.failed;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, SiteResource.MSG_THE_PRODUCT_HAS_NOT_UPDATED);
                }

                Session["ACTION_STATUS"] = actionStatus;
                return RedirectToAction("Edit", new { id = product.ID });
            }
            else
            {
                IsValid = false;
                foreach (var item in ModelState.Values)
                {
                    if (item.Errors.Count() > 0)
                    {
                        var errorItems = item.Errors.Where(f => !String.IsNullOrWhiteSpace(f.ErrorMessage)).ToList();
                        foreach (var erroritem in errorItems)
                        {
                            errorString += "<br />" + erroritem.ErrorMessage;
                        }
                    }
                }
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_WARNING, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Edit", new { id = product.ID });

        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            var DAO = new ProductDao();
            DAO.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: Admin/Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var DAO = new ProductDao();
                DAO.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}
