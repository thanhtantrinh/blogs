using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace OnlineShop.Areas.Admin.Controllers
{
    using Common;
    using OnlineShop.Filters;
    using OnlineShop.Resource;
    using StaticResources;
    using Model.EF;
    using Model.Dao;
    using Model.ViewModel;
    using OnlineShop.Helpers;

    [AuthLog(Roles = UserRoles.Admin)]
    public class UserController : BaseController
    {
        // GET: Admin/User
        //[HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 20)
        {
            var dao = new UserDao();
            AccountFilter filter = new AccountFilter();
            filter.CatalogueId = SiteConfiguration.CatalogueId;
            filter.SearchString = searchString;

            var model = dao.ListAllPaging(filter, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }
        [HttpGet]
        [AuthLog(Roles = UserRoles.Admin)]
        public ActionResult Create()
        {
            var model = new User();
            model.Status = nameof(StatusEntity.Active);
            return View();
        }

        [AuthLog(Roles = UserRoles.Admin)]
        public ActionResult Edit(int id=0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            bool IsValid = true;
            string message = String.Empty;

            if (id>0)
            {
                var user = new UserDao().ViewDetail(id);
                if (user != null)
                    return View(user);
                else
                {
                    IsValid = false;
                    actionStatus.ErrorStrings.Add(Resources.MSG_THE_USER_HAS_NOT_FOUND);
                    goto actionError;
                }
                   
            }
            else
            {
                IsValid = false;
                actionStatus.ErrorStrings.Add(Resources.MSG_THE_USER_HAS_NOT_FOUND);
                goto actionError;
            }
            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, actionStatus.ShowErrorStrings());
                Session[SessionName.ActionStatusLog] = actionStatus;
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        //[HasCredential(RoleID = "ADD_USER")]
        [AuthLog(Roles = UserRoles.Admin)]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                user.CatalogueId = SiteConfiguration.CatalogueId;
                user.CreatedBy = CurrentUser.ID;
                user.ModifiedBy = CurrentUser.ID;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Index");
        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        [AuthLog(Roles = UserRoles.Admin)]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                }                
                user.ModifiedBy = CurrentUser.ID;
                user.CatalogueId = SiteConfiguration.CatalogueId;

                var result = dao.Update(user);

                if (result)
                {
                    SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}