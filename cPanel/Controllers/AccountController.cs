using Common;
using cPanel.Filters;
using StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cPanel.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class AccountController :  BaseController
    {
        #region User manager
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Rule manager
        [HttpGet]
        public ActionResult Rules(int page = 1, int pageSize = 20)
        {
            var model = _accountRepo.GetRulesPaging(page, pageSize);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách quyền truy cập";
            return View(model);
        }

        [HttpPost]
        public ActionResult Rules()
        {

            return View();
        }

        public ActionResult RuleCreate()
        {
            return View();
        }

        public ActionResult RuleEdit()
        {
            return View();
        }
        #endregion

        #region Group Manager
        public ActionResult UserGroups(int page = 1, int pageSize = 20)
        {
            var model = _accountRepo.GetUserGroupPaging(page, pageSize);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách nhóm người dùng";
            return View(model);
        }

        public ActionResult UserGroupEdit(int Id=0)
        {
            return View();
        }

        public ActionResult UserGroupCreate()
        {
            return View();
        }
        #endregion

        #region User manager
        public ActionResult Users(int page = 1, int pageSize = 20)
        {
            var model = _accountRepo.GetUserPaging(page, pageSize);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách";
            return View(model);            
        }

        public ActionResult UserCreate()
        {
            return View();
        }
        public ActionResult UserEdit(long Id=0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;
            if (Id > 0)
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
        #endregion
    }
}