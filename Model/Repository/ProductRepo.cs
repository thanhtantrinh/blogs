using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Common;
using Model.EF;
using PagedList;
using AutoMapper;
using StaticResources;

namespace Model.Repository
{
    public class ProductRepo : BaseRepository
    {
        public ProductRepo() : base(conn)
        {

        }

        public List<ProductsView> getProducts()
        {
            var result = new List<ProductsView>();
            try
            {
                using (var db = new SqlConnection(conn))
                {
                    string sqlQuery = "";

                    result = db.Query<ProductsView>(sqlQuery).ToList();
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at getProducts at ProductRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }

        public List<v_Product> GetProducts(ProductFilter filter, string sortby = "", int Limit=10)
        {
            IQueryable<v_Product> model = entities.v_Product;
            try
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                {
                    string searchString = filter.SearchString.Trim();
                    model = model.Where(x => x.CategoryName.Contains(searchString));
                }

                if (filter.CatalogueId > 0)
                    model = model.Where(x => x.CatalogueId == filter.CatalogueId);

                if (filter.CategoryId > 0)
                {
                    model = model.Where(x => x.CategoryId == filter.CategoryId);
                }

                if (filter.Status.Count() > 0)
                {
                    model = model.Where(w => filter.Status.Contains(w.Status));
                }

                if (filter.IsNew.HasValue)
                {
                    model = model.Where(w => w.New == true);
                }

                if (filter.IsShowHome.HasValue)
                {
                    model = model.Where(w => w.ShowHome == true);
                }

                if (!String.IsNullOrWhiteSpace(sortby))
                {
                    if (sortby == "ViewCount")
                    {
                        model = model.OrderByDescending(x => x.ViewCount);
                    }
                    else
                    {
                        model = model.OrderByDescending(x => x.CreatedDate);
                    }
                }
                else
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetProducts at ProductRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            //return model.Take(Limit).ToList();
            return model.ToList();
        }


        public IEnumerable<v_Product> GetProductPaging(ProductFilter filter, int pageIndex = 1, int pageSize = 20, string sortby = "")
        {
            IQueryable<v_Product> model = entities.v_Product;
            try
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                {
                    string searchString = filter.SearchString.Trim();
                    model = model.Where(x => x.CategoryName.Contains(searchString));
                }

                if (filter.CatalogueId > 0)
                    model = model.Where(x => x.CatalogueId == filter.CatalogueId);

                if (filter.CategoryId > 0)
                {
                    model = model.Where(x => x.CategoryId == filter.CategoryId);
                }

                if (filter.Status.Count()>0)
                {
                    model = model.Where(w => filter.Status.Contains(w.Status));
                }

                if (filter.IsNew.HasValue)
                {
                    model = model.Where(w => w.New == true);
                }

                if (filter.IsShowHome.HasValue)
                {
                    model = model.Where(w => w.ShowHome == true);
                }

                if (!String.IsNullOrWhiteSpace(sortby))
                {
                    if (sortby == "ViewCount")
                    {
                        model = model.OrderByDescending(x => x.ViewCount);
                    }
                    else
                    {
                        model = model.OrderByDescending(x => x.CreatedDate);
                    }
                }
                else
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetProductPaging at ProductRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model.ToPagedList(pageIndex, pageSize);
        }

        public ProductsView GetProductById(long Id, out string message)
        {
            ProductsView result = new ProductsView();
            message = null;
            try
            {
                if (Id > 0)
                {
                    var product = entities.v_Product.FirstOrDefault(w => w.ProductId == Id);
                    if (product!=null)
                    {
                        result = Helper.ConvertProductToProductView(product);
                    }
                    else
                    {
                        message = Resources.MSG_THE_PRODUCT_CATEGORY_HAS_NOT_FOUND;
                    }
                }
                else
                {
                    message = Resources.MSG_THE_PRODUCT_CATEGORY_HAS_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetProductCategoryById at ProductCategoryRepo at Model.Repository";
                message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }

        public ProductsView Edit(ProductsView model, out string message)
        {
            ProductsView result = new ProductsView();
            message = null;
            try
            {
                var product = entities.Products.Find(model.ID);
                if (product != null)
                {
                    product.CatalogueId = model.CatalogueId;
                    product.CategoryId = model.CategoryID;
                    product.Name = product.Name.Trim();
                    if (String.IsNullOrWhiteSpace(model.MetaTitle))
                    {
                        product.MetaTitle = StringHelper.ToUnsignString(model.Name.Trim().ToLower());
                    }
                    product.Price = model.ProductPrice > 0 ? model.ProductPrice : 0;
                    product.PromotionPrice = model.PromotionPrice;
                    product.Quantity = model.Quantity;
                    product.Detail = model.Detail;
                    product.Description = model.Description;
                    product.Status = model.Status;
                    product.CategoryId = model.CategoryID;
                    product.Code = model.CategoryID.ToString() + "-" + model.ID.ToString();
                    product.IncludedVAT = product.IncludedVAT;

                    if (!String.IsNullOrWhiteSpace(model.MoreImages))
                    {
                        product.MoreImages = product.MoreImages;
                    }

                    if (model.Images != null && !String.IsNullOrWhiteSpace(model.Images.FileName))
                    {
                        product.Image = model.ID.ToString() + "_" + model.Images.FileName;
                    }
                    product.ShowHome = product.ShowHome;
                    //SEO
                    product.MetaDescriptions = model.MetaDescriptions;
                    product.MetaKeywords = model.MetaKeywords;
                    product.ModifiedBy = model.ModifiedBy;
                    product.ModifiedDate = DateTime.Now;

                    if (entities.SaveChanges() > 0)
                    {
                        result = GetProductById(product.Id, out message);
                    }
                    else
                    {
                        message = Resources.SYSTEM_ERROR_EXECUTE_SAVECHANGE_IN_ENTITY;
                    }
                }
                else
                {
                    message = Resources.MSG_THE_PRODUCT_CATEGORY_HAS_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Edit at ProductRepo at Model.Repository. ";
                message = StringHelper.Parameters2ErrorString(ex, model.ID);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }

        public ProductsView Create(ProductsView model, out string message)
        {      
            ProductsView result = new ProductsView();
            message = null;
            try
            {
                var product = Helper.ConvertProductViewToProduct(model);
                product.CreatedDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                entities.Products.Add(product);
                if (entities.SaveChanges() > 0)
                {
                    result = GetProductById(product.Id, out message);
                }
                else
                {
                    message = Resources.SYSTEM_ERROR_THE_UPDATING_WEBSITE_CONFI_HAS_BEEN_FINISHED;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Create at ProductRepo at Model.Repository. ";
                message = StringHelper.Parameters2ErrorString(ex);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }

    }
}
