using Common;
using cPanel.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ViewModel;
using cPanel.Resource;

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
            return View("Edit", model);            
        }
        [HttpPost]
        public ActionResult Create(CatalogueView model)
        {
            //load site configtion    
            ViewBag.Title = "Tạo catalog mới";
            return View(model);
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