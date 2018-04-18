using Common;
using cPanel.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ViewModel;
using cPanel.Resource;
using StaticResources;

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
            var model = _proCatRepo.GetCategoriesPaging(filter, page, pageSize, sortby);
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

        [HttpGet]
        public ActionResult ProductCategoryEdit(long Id)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;            
            bool IsValid = true;
            if (Id > 0)
            {
                string message = null;
                ProductCategoryView model = _proCatRepo.GetProductCategoryById(Id, out message);               
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
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("ProductCategory"); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCategoryEdit(ProductCategoryView model, string saveclose, string savenew)
        {
            ProductCategoryView remodel = new ProductCategoryView();
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;            
            string message = "";
            bool IsValid = true;
            if (ModelState.IsValid)
            {
                model.ModifiedBy = CurrentUser.ID;
                model.Language = currentCulture.ToString();
                remodel = _proCatRepo.Edit(model, out message);
                if (remodel != null && String.IsNullOrWhiteSpace(message))
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_PRODUCT_CATEGORY_HAS_UPDATED_SUCCESSFULLLY);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;
                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("ProductCategory");
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("ProductCategoryCreate");
                    else
                        return RedirectToAction("ProductCategoryEdit", new { Id = remodel.ID });
                }
                else
                {
                    IsValid = false;
                    actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_CATEGORY_HAS_UPDATED_UNSUCCESSFULLLY);
                    goto actionError;
                }
            }
            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + actionStatus.ShowErrorStrings());
                Session["ACTION_STATUS"] = actionStatus;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ProductCategoryCreate()
        {
            var model = new ProductCategoryView();
            model.CreatedByName = CurrentUser.Name;
            model.CreatedDate = DateTime.Now;
            model.ModifiedByName = CurrentUser.Name;
            model.ModifiedDate = DateTime.Now;
            return View("ProductCategoryEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCategoryCreate(ProductCategoryView model, string saveclose, string savenew)
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

                ProductCategoryView remodel = _proCatRepo.Create(model, out message);

                if (remodel != null && String.IsNullOrEmpty(message))
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CATEGORY_HAS_CREATED_SUCCESSFULLY);
                    Session["ACTION_STATUS"] = actionStatus;

                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("ProductCategory");
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("ProductCategoryCreate");
                    else
                        return RedirectToAction("ProductCategoryEdit", new { Id = remodel.ID });
                }
                else
                {                    
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, Resources.MSG_THE_CATEGORY_HAS_CREATED_UNSUCCESSFULLY);
                    Session["ACTION_STATUS"] = actionStatus;
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
                Session["ACTION_STATUS"] = actionStatus;
            }

            ViewBag.Title = Resources.CATEGORY_CREATE_A_NEW;
            ViewBag.Action = "CategoryCreate";
            return View("ProductCategoryEdit", model);

        }
        #endregion

        #region Product manager
        [HttpGet]
        public ActionResult Products(int page = 1, int pageSize = 20, string sortby = "")
        {
            ProductFilter filter = (ProductFilter)Session["ProductFilter"];
            if (filter == null)
            {
                filter = new ProductFilter();
                Session["ProductFilter"] = filter;
            }
            var model = _proRepo.GetProductPaging(filter, page, pageSize, sortby);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách nhóm sản phẩm";
            return View(model);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Products(ProductFilter filter)
        {
            if (filter != null)
                Session["ProductFilter"] = filter;
            else
                Session["ProductFilter"] = new ProductFilter();
            return RedirectToAction("Products");
        }
        [HttpGet]
        public ActionResult ProductEdit(long Id)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            bool IsValid = true;
            if (Id > 0)
            {
                string message = null;
                ProductsView model = _proRepo.GetProductById(Id, out message);
                if (model == null)
                {
                    IsValid = false;
                    actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_HAS_NOT_FOUND);
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
                actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_HAS_NOT_FOUND);
                goto actionError;
            }
            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, actionStatus.ShowErrorStrings());
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Products");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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