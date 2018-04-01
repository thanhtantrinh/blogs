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
using cPanel.Helpers;

namespace cPanel.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class CatalogueController : BaseController
    {        
        [HttpGet]        
        public ActionResult Index(int page = 1, int pageSize = 20)
        {
            CatalogueFilter filter = (CatalogueFilter)Session["CatalogueFilter"];
            if (filter == null)
            {
                filter = new CatalogueFilter();
                Session["CatalogueFilter"] = filter;
            }

            var model = _catalogueRepo.GetCataloguesPaging(filter, page, pageSize);

            ViewBag.Title = "Danh sách website";
            ViewBag.PageNumber = page;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(CatalogueFilter filter)
        {
            if (filter != null)
                Session["CatalogueFilter"] = filter;
            else
                Session["CatalogueFilter"] = new CatalogueFilter();
            return RedirectToAction("Index");            
        }
        [HttpGet]
        public ActionResult Create()
        {
            //load site configtion    
            var model = new CatalogueView();
            ViewBag.Title = "Tạo catalog mới";
            ViewBag.Action = "Create";
            return View("Edit", model);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatalogueView model, string saveclose = "", string savenew="")
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string message = "";
            CatalogueView result = new CatalogueView();

            if (ModelState.IsValid)
            {
                model.CreatedById = CurrentUser.UserID;
                model.ModifiedById = CurrentUser.UserID;
                result = _catalogueRepo.CreateCatalogue(model, out message);

                if(result!=null && result.Id > 0)
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CATEGORY_HAS_CREATED_SUCCESSFULLY);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;

                    if (!String.IsNullOrWhiteSpace(saveclose))
                    {
                        return RedirectToAction("Index");
                    }
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("Create");
                    else
                    {
                        return RedirectToAction("Edit", new { id = result.Id });                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", Resources.MSG_THE_CATEGORY_HAS_CREATED_UNSUCCESSFULLY);
                }               

            }

            //load site configtion    
            ViewBag.Title = "Tạo catalog mới";
            ViewBag.Action = "Create";
            return View("Edit", model);
        }
        [HttpGet]
        public ActionResult Edit(int Id=0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;            
            string message = "";            
            
            var model = _catalogueRepo.GetCatalogue(Id, out message);

            if (model!=null && String.IsNullOrWhiteSpace(message))
            {
                ViewBag.Title =  String.Format(Resources.LABEL_UPDATE,model.SiteName);
                return View(model);
            }
            else
            {                
                actionStatus.ErrorReason = String.Format(message);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CatalogueView model, string saveclose, string savenew)
        {
            CatalogueView remodel = new CatalogueView();
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                model.ModifiedById = CurrentUser.ID;                

                remodel = _catalogueRepo.UpdateCatalogue(model, out message);              
                if (remodel!=null && String.IsNullOrWhiteSpace(message))
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_SITE_CONFIGUARATION_HAS_BEEN_UPDATED_SUCCESSFULLY);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;
                    if (!String.IsNullOrEmpty(saveclose))                    
                        return RedirectToAction("Index");                    
                    else if (!String.IsNullOrWhiteSpace(savenew))
                    {
                        return RedirectToAction("Create");
                    }                        
                    else                    
                        return RedirectToAction("Edit", new { Id = remodel.Id });                                     
                }
                else
                {
                    IsValid = false;
                    errorString = Resources.MSG_THE_SITE_CONFIGUARATION_HAS_BEEN_UPDATED_UNSUCCESSFULLY;
                    goto actionError;
                }
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return RedirectToAction("Index");
        }
    }
}