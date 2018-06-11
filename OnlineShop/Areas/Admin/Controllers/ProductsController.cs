using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.Dao;
using Model.ViewModel;
using System.IO;
using System.Web.Script.Serialization;
using Common;
using OnlineShop.Helpers;
using OnlineShop.Resource;
using StaticResources;
using OnlineShop.Filters;

namespace OnlineShop.Areas.Admin.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class ProductsController : BaseController
    {
        private ProductDao DAO = new ProductDao();
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 20, string sortby = "")
        {
            ProductFilter filter = (ProductFilter)Session["ProductFilter"];
            if (filter == null)
            {
                filter = new ProductFilter();
                Session["ProductFilter"] = filter;
            }
            var model = _proRepo.GetProductPaging(filter, page, pageSize, sortby);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách nhóm sản phẩm";
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ProductFilter filter)
        {
            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.SearchString))
                {
                    filter.SearchString = filter.SearchString.Trim();
                }

                Session["ProductFilter"] = filter;
            }   
            else
                Session["ProductFilter"] = new ProductFilter();
            return RedirectToAction("Index");
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            var model = new ProductsView();
            model.Status = nameof(StatusEntity.Active);
            model.ProductDetail = new List<ProductDetailModel>() { new ProductDetailModel() };
            return View(model);
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductsView product, string saveclose, string savenew)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;
            if (ModelState.IsValid)
            {
                var DAO = new ProductDao();
                product.CatalogueId = SiteConfiguration.CatalogueId;
                //product.CatalogueId = SiteConfiguration.CatalogueId;
                product.CreatedBy = CurrentUser.UserID;               

                if (product.Images != null && !String.IsNullOrWhiteSpace(product.Images.FileName))
                {
                    string path = Path.Combine(Server.MapPath("~/Images/Products/large"), Path.GetFileName(product.MetaTitle + "_" + product.Images.FileName));
                    product.Images.SaveAs(path);
                    //resize cho vao tung thu muc
                    ImageHelper.CreateThumbnail(150, 150, path, Server.MapPath("~/Images/Products/small/"));
                    ImageHelper.CreateThumbnail(80, 80, path, Server.MapPath("~/Images/Products/min/"));
                    ImageHelper.CreateThumbnail(500, 500, path, Server.MapPath("~/Images/Products/medium/"));
                }

                var result = DAO.Add(product);

                if (result!=null && result.Id>0)
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, SiteResource.MSG_THE_PRODUCT_HAS_BEEN_CREATED);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    //Session[SessionName.ActionStatusLog] = actionStatus;
                    //return RedirectToAction("Index");
                    Session[SessionName.ActionStatusLog] = actionStatus;
                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("Index");
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("Create");
                    else
                        return RedirectToAction("Edit", new { Id = result.Id });
                }
                else
                {
                    actionStatus.ErrorStrings.Add(SiteResource.MSG_THE_PRODUCT_HAS_NOT_BEEN_CREATED);
                    goto actionError;
                }

            }
            else
            {
                IsValid = false;
                foreach (var item in ModelState.Values)
                {
                    if (item.Errors.Count() > 0)
                    {
                        var errorItems = item.Errors.Where(f => !String.IsNullOrWhiteSpace(f.ErrorMessage)).ToList();
                        foreach (var erroritem in errorItems)
                        {
                            errorString += "<br />" + erroritem.ErrorMessage;
                        }
                    }
                }
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_WARNING, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            bool IsValid = true;
            string message = String.Empty;
            ProductsView model = new ProductsView();
            if (id > 0)
            {
                model = DAO.Find(id);
                if (model == null)
                {
                    IsValid = false;
                    actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_HAS_NOT_FOUND);
                    goto actionError;
                    //return RedirectToAction("Index");
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
                actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_HAS_NOT_FOUND);
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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductsView product, string saveclose, string savenew)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;
            if (ModelState.IsValid)
            {
                var DAO = new ProductDao();

                if (product.Images != null && !String.IsNullOrWhiteSpace(product.Images.FileName))
                {
                    string path = Path.Combine(Server.MapPath("~/Images/Products/large"), Path.GetFileName(product.MetaTitle + "_" + product.Images.FileName));
                    product.Images.SaveAs(path);
                    //resize cho vao tung thu muc
                    ImageHelper.CreateThumbnail(150, 150, path, Server.MapPath("~/Images/Products/small/"));
                    ImageHelper.CreateThumbnail(80, 80, path, Server.MapPath("~/Images/Products/min/"));
                    ImageHelper.CreateThumbnail(500, 500, path, Server.MapPath("~/Images/Products/medium/"));
                }
                product.CatalogueId = SiteConfiguration.CatalogueId;
                product.ModifiedBy = CurrentUser.UserID;
                //product.ModifiedDate = DateTime.Now;
                var result = DAO.Update(product);
                if (result)
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, SiteResource.MSG_THE_PRODUCT_HAS_BEEN_UPDATED);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session[SessionName.ActionStatusLog] = actionStatus;
                    if (!String.IsNullOrEmpty(saveclose))
                        return RedirectToAction("Index");
                    else if (!String.IsNullOrWhiteSpace(savenew))
                        return RedirectToAction("Create");
                    else
                        return RedirectToAction("Edit", new { Id = product.ID });

                    //return RedirectToAction("Index");
                }
                else
                {
                    actionStatus.ActionStatus = ResultSubmit.failed;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, SiteResource.MSG_THE_PRODUCT_HAS_NOT_UPDATED);
                }
                Session[SessionName.ActionStatusLog] = actionStatus;
                return RedirectToAction("Edit", new { id = product.ID });
            }
            else
            {
                IsValid = false;
                foreach (var item in ModelState.Values)
                {
                    if (item.Errors.Count() > 0)
                    {
                        var errorItems = item.Errors.Where(f => !String.IsNullOrWhiteSpace(f.ErrorMessage)).ToList();
                        foreach (var erroritem in errorItems)
                        {
                            errorString += "<br />" + erroritem.ErrorMessage;
                        }
                    }
                }
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_WARNING, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session[SessionName.ActionStatusLog] = actionStatus;
            }
            return RedirectToAction("Edit", new { id = product.ID });

        }
        [HttpGet]
        public ActionResult CreateProductDetail(long ProductId = 0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            bool IsValid = true;
            if (ProductId > 0)
            {
                DAO.CreateGetProductDetail(ProductId);
                return RedirectToAction("Edit", new { id = ProductId });
            }
            else
            {
                IsValid = false;
                actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_HAS_NOT_FOUND);
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
        [HttpGet]
        public ActionResult DeleteProductDetail(long ProductId = 0,long productDetailId=0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            bool IsValid = true;
            if (ProductId > 0 && productDetailId>0)
            {
                DAO.DeleteProductDeteail(productDetailId);
                actionStatus.ActionStatus = ResultSubmit.success;
                actionStatus.Message = String.Format(SiteResource.HTML_ALERT_SUCCESS, SiteResource.MSG_THE_PRODUCT_WEIGHT_PRICE_HAS_BEEN_REMOVED);
                return RedirectToAction("Edit", new { id = ProductId });
            }
            else
            {
                IsValid = false;
                actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_HAS_NOT_FOUND);
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
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        public ActionResult SetLockProduct(int Id = 0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string message = "";
            bool IsValid = true;
            ProductsView model = _proRepo.GetProductById(Id, out message);
            if (model != null)
            {
                model.Status = nameof(StatusEntity.Locked);
                model = _proRepo.Edit(model, out message);

                if (model != null && String.IsNullOrWhiteSpace(message))
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_PRODUCT_HAS_UPDATED_SUCCESSFULLLY);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                IsValid = false;
                actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_HAS_NOT_FOUND);
                goto actionError;
            }
            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, actionStatus.ShowErrorStrings());
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Index");
        }
        public ActionResult SetDeletedProduct(int Id = 0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string message = "";
            bool IsValid = true;
            ProductsView model = _proRepo.GetProductById(Id, out message);
            if (model != null && Id > 0)
            {
                model.Status = nameof(StatusEntity.Deleted);
                model = _proRepo.Edit(model, out message);
                if (model != null && String.IsNullOrWhiteSpace(message))
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, Resources.MSG_THE_PRODUCT_HAS_UPDATED_SUCCESSFULLLY);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                IsValid = false;
                actionStatus.ErrorStrings.Add(Resources.MSG_THE_PRODUCT_HAS_NOT_FOUND);
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, actionStatus.ShowErrorStrings());
                Session["ACTION_STATUS"] = actionStatus;
            }
            return RedirectToAction("Index");
        }
        public ActionResult ImportProduct()
        {
            List<ProductsView> products = new List<ProductsView>();
            try
            {
                //Read the contents of CSV file.
                string filePath = string.Empty;
                string path = Server.MapPath("~/App_Data/");

                filePath = path + Path.GetFileName("thuc-pham-chay-truyen-thong.csv");

                string csvData = System.IO.File.ReadAllText(filePath);
                var DAO = new ProductDao();

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        string productName = row.Split(',')[0];
                        double price = Convert.ToInt64(row.Split(',')[1]);
                        double weight = Convert.ToInt32(row.Split(',')[3]);

                        var proDetails = new List<ProductDetailModel>();
                        proDetails.Add(new ProductDetailModel() { ProductPrice = price, ProductWeight = weight, PriceTypeId = 1 });

                        products.Add(new ProductsView
                        {
                            Name = productName,
                            MetaTitle = StringHelper.ToUnsignString(productName.Trim().ToLower()),
                            ProductDetail = proDetails,
                            CategoryID = Convert.ToInt32(row.Split(',')[2]),
                            CatalogueId = SiteConfiguration.CatalogueId,
                            Language = Session[CommonConstants.CurrentCulture].ToString(),
                            Description = "",
                            Detail = "",
                            Status = nameof(StatusEntity.Active),
                            CreatedBy = 2,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = 2,
                            ModifiedDate = DateTime.Now,
                            ViewCount = 0,

                        });
                    }
                }

                bool isTesing = false;
                if (products.Count > 0 && !isTesing)
                {
                    foreach (var item in products)
                    {
                        var result = DAO.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            //return View(products);

            return RedirectToAction("Index");
        }
    }
}
