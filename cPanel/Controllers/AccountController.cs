using Common;
using cPanel.Filters;
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

    }
}