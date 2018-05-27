using Common;
using Model.Dao;
using Model.EF;
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
    public class ContentController : BaseController
    {
        private ContentDao dao = new ContentDao();

        [HttpGet]
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            ContentFilter filter = (ContentFilter)Session["ContentFilter"];
            if (filter == null)
            {
                filter = new ContentFilter();
                Session["ContentFilter"] = filter;
            }
            filter.CatalogueId = SiteConfiguration.CatalogueId;
            var model = dao.ListAllPaging(filter, page, pageSize);
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ContentFilter filter)
        {
            if (filter != null)
                Session["ContentFilter"] = filter;
            else
                Session["ContentFilter"] = new ContentFilter();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create()
        {
            ContentViewModel model = new ContentViewModel();

            model.CreatedByName = CurrentUser.Name;
            model.CreatedDate = DateTime.Now;
            model.ModifiedByName = CurrentUser.Name;
            model.ModifiedDate = DateTime.Now;
            ViewBag.Title = Resources.LABEL_CREATE_NEW_CONTENT;
            ViewBag.Action = "Create";
            return View("Edit", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContentViewModel model, string saveclose)
        {
            ContentViewModel remodel = new ContentViewModel();
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {                
                model.CreatedBy = CurrentUser.UserID;
                model.ModifiedBy = CurrentUser.UserID;

                //var culture = Session[CommonConstants.CurrentCulture];
                model.Language = currentCulture.ToString();
                model.CatalogueId = SiteConfiguration.CatalogueId;
                remodel = dao.Create(model, out message);

                if (remodel != null && String.IsNullOrEmpty(message))
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CONTENT_HAS_CREATED_SUCCESSFULLY);
                    Session[SessionName.ActionStatusLog] = actionStatus;

                    if (!String.IsNullOrEmpty(saveclose))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Edit", new { id = remodel.ID });
                    }
                }
                else
                {
                    IsValid = false;
                    errorString = Resources.MSG_THE_CONTENT_HAS_CREATED_UNSUCCESSFULLY;
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
                Session[SessionName.ActionStatusLog] = actionStatus;
            }
            ViewBag.Title = Resources.LABEL_CREATE_NEW_CONTENT;
            return View("Edit", model);
        }
        [HttpGet]
        
        public ActionResult Edit(long id=0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;
            ContentViewModel model = dao.GetByID(id);

            if (model == null)
            {
                IsValid = false;
                errorString += Resources.MSG_THE_CONTENT_HAS_NOT_FOUND;
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
                Session[SessionName.ActionStatusLog] = actionStatus;
            }
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Edit(ContentViewModel model, string saveclose)
        {
            ContentViewModel remodel = new ContentViewModel();
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                model.ModifiedBy = CurrentUser.UserID;
                model.CatalogueId = SiteConfiguration.CatalogueId;
                model.Language = currentCulture.ToString();                
                remodel = dao.Update(model, out message);

                if (remodel != null && String.IsNullOrEmpty(message))
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.Message = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CONTENT_HAS_UPDATED_SUCCESSFULLY);
                    Session[SessionName.ActionStatusLog] = actionStatus;
                    if (!String.IsNullOrEmpty(saveclose))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Edit",new { id= remodel.ID});
                    }                    
                }
                else
                {
                    //ModelState.AddModelError("", Resources.InsertCategoryFailed);
                    IsValid = false;
                    errorString = Resources.MSG_THE_CONTENT_HAS_UPDATED_UNSUCCESSFULLY;
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
                Session[SessionName.ActionStatusLog] = actionStatus;
            }
            ViewBag.Title = String.Format(Resources.LABEL_UPDATE, model.Name);
            return View("Edit", model);
        } 
    }
}