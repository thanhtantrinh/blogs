using Common;
using Model.Dao;
using Model.EF;
using Model.ViewModel;
using OnlineShop.Common;
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
            
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                model.CreatedBy = CurrentUser.UserID;
                var culture = Session[Common.CommonConstants.CurrentCulture];
                model.Language = culture.ToString();
                new ContentDao().Create(model);
                return RedirectToAction("Index");
            }            
            return View();
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
                Session["ACTION_STATUS"] = actionStatus;
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
                remodel = dao.Update(model, out message);

                if (remodel != null && String.IsNullOrEmpty(message))
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CONTENT_HAS_UPDATED_SUCCESSFULLY);
                    Session["ACTION_STATUS"] = actionStatus;

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
                Session["ACTION_STATUS"] = actionStatus;
            }
            ViewBag.Title = String.Format(Resources.LABEL_UPDATE, model.Name);
            return View("Edit", model);
        } 
    }
}