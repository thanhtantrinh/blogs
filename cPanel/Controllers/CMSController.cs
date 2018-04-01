using Common;
using cPanel.Filters;
using cPanel.Resource;
using Model.ViewModel;
using StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cPanel.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class CMSController : BaseController
    {
        // GET: CMS
        public ActionResult Index()
        {
            return View();
        }

        #region Content Manager
        public ActionResult ContentList(int page = 1, int pageSize = 20,string sortby="")
        {
            ContentFilter filter = (ContentFilter)Session["ContentFilter"];
            if (filter == null)
            {
                filter = new ContentFilter();
                Session["ContentFilter"] = filter;
            }
            var model = _contentRepo.GetContentPaging(filter, page, pageSize, sortby);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách bài viết";
            return View(model);          
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ContentList(ContentFilter filter)
        {
            if (filter != null)
                Session["ContentFilter"] = filter;
            else
                Session["ContentFilter"] = new ContentFilter();

            return RedirectToAction("ContentList");
        }
        [HttpGet]
        public ActionResult ContentEdit(long Id=0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;

            if (Id>0)
            {
                ContentViewModel model = _contentRepo.GetContentById(Id);
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
            }
            else
            {
                IsValid = false;
                errorString = Resources.MSG_THE_CONTENT_HAS_NOT_FOUND;
                goto actionError;
            }


            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("ContentList");
            
        }

        public ActionResult ContentCreate()
        {
            return View();
        }

        #endregion

        #region Catagory Manager
        public ActionResult CategoryList()
        {
            return View();
        }

        public ActionResult CategoryEdit()
        {
            return View();
        }
        public ActionResult CategoryCreate()
        {
            return View();
        }
        #endregion;
    }
}