using AutoMapper;
using Common;
using Model.Dao;
using Model.EF;
using Model.Repository;
using Model.ViewModel;
using OnlineShop.Filters;
using OnlineShop.Helpers;
using OnlineShop.Resource;
using StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class ProductCategoryController : BaseController
    {

        // GET: Admin/ProductCategory
        public ActionResult Index(int? page, int pageSize = 20, string sortBy = "")
        {
            //var dao = new ProductCategoryDao();      
            //var model = dao.ListAllPaging(searchString, page, pageSize, catId);
            ProductCategoryFilter filter = (ProductCategoryFilter)Session["ProductCategoryFilter"];
            if (filter == null)
            {
                filter = new ProductCategoryFilter();
                Session["ProductCategoryFilter"] = filter;
            }
            filter.CatalogueId = SiteConfiguration.CatalogueId;
            var result = _proCatRepo.GetCategoriesPaging(filter, page ?? 1, pageSize, sortBy);
            ViewBag.Filter = filter;
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(ProductCategoryFilter filter, string searchBy = "")
        {
            if (filter != null)
                Session["ProductCategoryFilter"] = filter;
            else
                Session["ProductCategoryFilter"] = new ProductCategoryFilter();

            return RedirectToAction("Index");
        }

        // GET: Admin/ProductCategory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProductCategoryView();
            model.CreatedByName = CurrentUser.Name;
            model.CreatedDate = DateTime.Now;
            model.ModifiedByName = CurrentUser.Name;
            model.ModifiedDate = DateTime.Now;
            model.Status = nameof(StatusEntity.Active);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryView model, string saveclose, string savenew)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                model.Language = currentCulture.ToString();
                model.CreatedBy = CurrentUser.UserID;
                model.ModifiedBy = CurrentUser.UserID;
                model.CatalogueId = SiteConfiguration.CatalogueId;
                ProductCategoryView remodel = _proCatRepo.Create(model, out message);

                if (remodel != null && String.IsNullOrEmpty(message))
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_PRODUCT_CATEGORY_HAS_CREATED_SUCCESSFULLLY);
                    Session[SessionName.ActionStatusLog] = actionStatus;

                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("Index");
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("Create");
                    else
                        return RedirectToAction("Edit", new { Id = remodel.ID });
                }
                else
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, Resources.MSG_THE_PRODUCT_CATEGORY_HAS_CREATED_UNSUCCESSFULLLY);
                    Session[SessionName.ActionStatusLog] = actionStatus;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                IsValid = false;
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, Resources.MSG_ERROR_ENTER_DATA_FOR_FORM + actionStatus.ShowErrorStrings());
                Session[SessionName.ActionStatusLog] = actionStatus;
            }

            ViewBag.Title = Resources.MSG_THE_PRODUCT_CATEGORY_HAS_CREATED_UNSUCCESSFULLLY;
            ViewBag.Action = "Create";
            return View("Edit", model);
        }

        // GET: Admin/ProductCategory/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            bool IsValid = true;          
            var dao = new ProductCategoryDao();
            ProductCategoryView model = new ProductCategoryView();
            if (id > 0)
            {                      
                model = dao.Find(id);
                if (model == null)
                {
                    IsValid = false;
                    actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_CATEGORY_HAS_NOT_FOUND);
                    goto actionError;
                }
                else
                {
                    ViewBag.Title = String.Format(Resources.LABEL_UPDATE, model.Name);
                    return View(model);
                }
            }
            else
            {
                IsValid = false;
                actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_CATEGORY_HAS_NOT_FOUND);
                goto actionError;
            }
            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, actionStatus.ShowErrorStrings());
                Session[SessionName.ActionStatusLog] = actionStatus;
            }
            
            return View(model);
        }

        // POST: Admin/ProductCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductCategoryView model, string saveclose, string savenew)
        {

            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string message = String.Empty;
            bool IsValid = true;

            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                model.Language = currentCulture.ToString();
                model.ModifiedBy = CurrentUser.UserID;
                model.CatalogueId = SiteConfiguration.CatalogueId;
                var result = _proCatRepo.Edit(model, out message);
                var dao = new ProductCategoryDao();
                Mapper.Initialize(cfg => cfg.CreateMap<ProductCategoryView, ProductCategory>());
                var data = Mapper.Map<ProductCategoryView, ProductCategory>(model);
                if (result!=null&& String.IsNullOrEmpty(message))
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_PRODUCT_CATEGORY_HAS_UPDATED_SUCCESSFULLLY);
                    Session[SessionName.ActionStatusLog] = actionStatus;
                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("Index");
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("Create");
                    else
                        return RedirectToAction("Edit", new { Id = model.ID });
                }
                else
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, Resources.MSG_THE_PRODUCT_CATEGORY_HAS_UPDATED_UNSUCCESSFULLLY);
                    Session[SessionName.ActionStatusLog] = actionStatus;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                IsValid = false;
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, Resources.MSG_ERROR_ENTER_DATA_FOR_FORM + actionStatus.ShowErrorStrings());
                Session[SessionName.ActionStatusLog] = actionStatus;
            }

            return View(model);
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
