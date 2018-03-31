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
    public class CatalogueController : BaseController
    {        
        [HttpGet]        
        public ActionResult Index(int page = 1, int pageSize = 10)
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
        public ActionResult Create(CatalogueView model, string save="")
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
                    if (String.IsNullOrWhiteSpace(save))
                    {
                        return RedirectToAction("Edit", new { id = result.Id });
                    }
                    else
                    {
                        return RedirectToAction("Create");
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
                ViewBag.Title = model.SiteName;
                return View(model);
            }
            else
            {                
                actionStatus.ErrorReason = String.Format(message);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Index");
        }
    }
}