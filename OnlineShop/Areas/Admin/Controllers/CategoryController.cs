using Model.Dao;
using Model.EF;
using OnlineShop.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StaticResources;
using OnlineShop.Resource;
using Model.ViewModel;
using OnlineShop.Filters;
using Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class CategoryController : BaseController
    {
        private CategoryDao _categoryDao = new CategoryDao();

        [HttpGet]
        // GET: Admin/Category
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            CategoryFilter filter = (CategoryFilter)Session["CategoryFilter"];
            if (filter == null)
            {
                filter = new CategoryFilter();
                Session["CategoryFilter"] = filter;
            }

            var model = _categoryDao.ListAllPaging(filter, page, pageSize);

            
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(CategoryFilter filter)
        {
            
            if (filter != null)
                Session["CategoryFilter"] = filter;
            else
                Session["CategoryFilter"] = new CategoryFilter();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryView model = new CategoryView();
            ViewBag.Title = Resources.CATEGORY_CREATE_A_NEW;
            return View("Edit", model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CategoryView model)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstants.CurrentCulture];

                model.Language = currentCulture.ToString();
                model.CreatedBy = CurrentUser.UserID;
                
                CategoryView newModel = new CategoryDao().Insert(model, out message);
                if (newModel !=null && String.IsNullOrEmpty(message))
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CATEGORY_HAS_CREATED_SUCCESSFULLY);
                    Session["ACTION_STATUS"] = actionStatus;
                    return RedirectToAction("Index");
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
            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(long id=0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;
            var model = _categoryDao.GetByID(id);
            if (model == null)
            {
                IsValid = false;
                errorString += Resources.MSG_THE_CATEGORY_HAS_NOT_FOUND;
                goto actionError;
            }
            else
            {
                ViewBag.Title = String.Format(Resources.LABEL_UPDATE, model.Name);
                return View(model);
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CategoryView model)
        {
            CategoryView remodel = new CategoryView();
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                //var currentCulture = Session[Common.CommonConstants.CurrentCulture];                
                model.ModifiedBy = CurrentUser.UserID;
                remodel = _categoryDao.Edit(model, out message);
                if (remodel!=null && String.IsNullOrEmpty(message) )
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CATEGORY_HAS_UPDATED_SUCCESSFULLY);
                    Session["ACTION_STATUS"] = actionStatus;
                    return RedirectToAction("Index");
                }
                else
                {
                    //ModelState.AddModelError("", Resources.InsertCategoryFailed);
                    errorString = Resources.MSG_THE_CATEGORY_HAS_UPDATED_UNSUCCESSFULLY;
                    goto actionError;
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
            ViewBag.Title = String.Format(Resources.LABEL_UPDATE, model.Name);
            return View("Edit", model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}