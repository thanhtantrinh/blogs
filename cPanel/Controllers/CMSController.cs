using Common;
using cPanel.Filters;
using Model.ViewModel;
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

        public ActionResult ContentEdit()
        {
            return View();
        }

        public ActionResult ContentCreate()
        {
            return View();
        }

        #endregion

        #region Catagory Manager

        #endregion;
    }
}