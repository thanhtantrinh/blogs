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
        [HttpGet]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContentEdit(ContentViewModel model, string saveclose, string savenew)
        {
            ContentViewModel remodel = new ContentViewModel();
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                model.ModifiedBy = CurrentUser.ID;
                model.Language = Session[CommonConstants.CurrentCulture].ToString();

                remodel = _contentRepo.Update(model, out message);
                if (remodel != null && String.IsNullOrWhiteSpace(message))
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CONTENT_HAS_UPDATED_SUCCESSFULLY);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;

                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("ContentList");
                    else if (!String.IsNullOrWhiteSpace(savenew))                    
                        return RedirectToAction("ContentCreate");                    
                    else
                        return RedirectToAction("ContentEdit", new { Id = remodel.ID });
                }
                else
                {
                    IsValid = false;
                    errorString = Resources.MSG_THE_CONTENT_HAS_UPDATED_UNSUCCESSFULLY;
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
        public ActionResult ContentCreate()
        {
            ContentViewModel model = new ContentViewModel();
            model.ModifiedByName = CurrentUser.DisplayName;
            model.CreatedByName = CurrentUser.DisplayName;
            model.Status = nameof(StatusEntity.Active);
            model.ModifiedDate = DateTime.Now;
            model.CreatedDate = DateTime.Now;
            model.Language = currentCulture;
            
            ViewBag.Title = "Tạo bài viết mới";
            ViewBag.Action = "ContentCreate";
            return View("ContentEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContentCreate(ContentViewModel model, string saveclose, string savenew)
        {
            ContentViewModel remodel = new ContentViewModel();
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                model.ModifiedBy = CurrentUser.ID;
                model.CreatedBy = CurrentUser.ID;
                model.ModifiedDate = DateTime.Now;
                model.CreatedDate = DateTime.Now;
                model.Language = currentCulture;

                remodel = _contentRepo.Create(model, out message);
                if (remodel != null && String.IsNullOrWhiteSpace(message))
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CONTENT_HAS_CREATED_SUCCESSFULLY);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;

                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("ContentList");
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("ContentCreate");
                    else
                        return RedirectToAction("ContentEdit", new { Id = remodel.ID });
                }
                else
                {
                    IsValid = false;
                    errorString = Resources.MSG_THE_CONTENT_HAS_CREATED_UNSUCCESSFULLY;
                    goto actionError;
                }
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }

            ViewBag.Title = "Tạo bài viết mới";
            ViewBag.Action = "ContentCreate";
            return View("ContentEdit", model);
        }

        #endregion

        #region Catagory Manager
        [HttpGet]
        public ActionResult CategoryList(int page = 1, int pageSize = 20, string sortby = "")
        {
            CategoryFilter filter = (CategoryFilter)Session["CategoryFilter"];
            if (filter == null)
            {
                filter = new CategoryFilter();
                Session["CategoryFilter"] = filter;
            }
            var model = _categoryRepo.GetCategoriesPaging(filter, page, pageSize, sortby);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách danh mục";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryList(CategoryFilter filter)
        {
            if (filter != null)
                Session["CategoryFilter"] = filter;
            else
                Session["CategoryFilter"] = new CategoryFilter();

            return RedirectToAction("CategoryList");
        }


        [HttpGet]
        public ActionResult CategoryEdit(long Id=0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;

            if (Id > 0)
            {
                CategoryView model = _categoryRepo.GetCategoryById(Id);
                if (model == null)
                {
                    IsValid = false;
                    errorString += Resources.MSG_THE_CATEGORY_HAS_NOT_FOUND;
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
                errorString = Resources.MSG_THE_CATEGORY_HAS_NOT_FOUND;
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("CategoryList");            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit(CategoryView model, string saveclose, string savenew)
        {
            CategoryView remodel = new CategoryView();
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                if (model.ID > 0)
                {                    
                    model.ModifiedBy = CurrentUser.UserID;
                    model.Language = Session[CommonConstants.CurrentCulture].ToString();
                    remodel = _categoryRepo.Edit(model, out message);
                    if (remodel != null && String.IsNullOrEmpty(message))
                    {
                        actionStatus.ActionStatus = ResultSubmit.success;
                        actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CATEGORY_HAS_UPDATED_SUCCESSFULLY);
                        Session["ACTION_STATUS"] = actionStatus;

                        if (!String.IsNullOrEmpty(saveclose))
                            return RedirectToAction("CategoryList");
                        else if (!String.IsNullOrWhiteSpace(savenew))
                            return RedirectToAction("CategoryCreate");
                        else
                            return RedirectToAction("CategoryEdit", new { Id = remodel.ID });                      

                    }
                    else
                    {
                        //ModelState.AddModelError("", Resources.InsertCategoryFailed);
                        errorString = Resources.MSG_THE_CATEGORY_HAS_UPDATED_UNSUCCESSFULLY;
                        goto actionError;
                    }
                }
                else
                {
                    errorString = Resources.MSG_THE_CATEGORY_HAS_NOT_FOUND;
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
            return View("CategoryEdit", model);
        }
        [HttpGet]
        public ActionResult CategoryCreate()
        {
            CategoryView model = new CategoryView();
            model.ModifiedByName = CurrentUser.DisplayName;
            model.CreatedByName = CurrentUser.DisplayName;
            model.ModifiedDate = DateTime.Now;
            model.CreatedDate = DateTime.Now;
            model.Language = Session[CommonConstants.CurrentCulture].ToString();
            model.Status = nameof(StatusEntity.Active);

            ViewBag.Title = Resources.CATEGORY_CREATE_A_NEW;
            ViewBag.Action = "CategoryCreate";
            return View("CategoryEdit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate(CategoryView model, string saveclose, string savenew)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            string message = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                model.Language = currentCulture.ToString();
                model.CreatedBy = CurrentUser.UserID;
                model.ModifiedBy = CurrentUser.UserID;

                CategoryView remodel = _categoryRepo.Create(model, out message);

                if (remodel != null && String.IsNullOrEmpty(message))
                {
                    actionStatus.ActionStatus = ResultSubmit.success;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_CATEGORY_HAS_CREATED_SUCCESSFULLY);
                    Session["ACTION_STATUS"] = actionStatus;

                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("CategoryList");
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("CategoryCreate");
                    else
                        return RedirectToAction("CategoryEdit", new { Id = remodel.ID });                    
                }
                else
                {
                    //ModelState.AddModelError("", Resources.MSG_THE_CATEGORY_HAS_CREATED_UNSUCCESSFULLY);                    
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, Resources.MSG_THE_CATEGORY_HAS_CREATED_UNSUCCESSFULLY);
                    Session["ACTION_STATUS"] = actionStatus;
                    return RedirectToAction("Index");
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

            ViewBag.Title = Resources.CATEGORY_CREATE_A_NEW;
            ViewBag.Action = "CategoryCreate";
            return View("CategoryEdit", model);
        }
        #endregion;
    }
}