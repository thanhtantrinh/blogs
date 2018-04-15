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

        [HttpGet]
        public ActionResult ProductCategoryCreate()
        {
            var model = new ProductCategoryView();
            model.CreatedByName = CurrentUser.Name;
            model.CreatedDate = DateTime.Now;
            model.ModifiedByName = CurrentUser.Name;
            model.ModifiedDate = DateTime.Now;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCategoryCreate(ProductCategoryView model, string saveclose, string savenew)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                model.Language = currentCulture.ToString();
                model.CreatedBy = CurrentUser.UserID;
                model.ModifiedBy = CurrentUser.UserID;

                ProductCategoryView remodel = _proRepo.Create(model, out message);

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
                    //ModelState.AddModelError("", Resources.MSG_THE_CATEGORY_HAS_CREATED_UNSUCCESSFULLY);                    
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
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, Resources.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }

            ViewBag.Title = Resources.CATEGORY_CREATE_A_NEW;
            ViewBag.Action = "CategoryCreate";
            return View("ProductCategoryEdit", model);
            
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