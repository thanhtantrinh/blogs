using Common;
using Dapper;
using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticResources;

namespace Model.Repository
{
    public class ProductCategoryRepo: BaseRepository
    {
        public ProductCategoryRepo() : base(conn)
        {

        }

        public ProductCategoryView GetProductCategoryById(long Id, out string message)
        {
            ProductCategoryView result = new ProductCategoryView();
            message = null;
            try
            {
                if (Id > 0)
                {
                    var categoryOfProduct = entities.v_CategoryOfProduct.FirstOrDefault(w => w.ID == Id);
                    result = Helper.ConvertProductCategoryToProductCategoryView(categoryOfProduct);
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
        public Tuple<List<ProductCategoryView>, int> getCategories(ProductCategoryFilter filter, int pageNumber = 1, int pageSize = 10, string SortBy = "")
        {
            var result = new List<ProductCategoryView>();
            int totalItem = 0;
            try
            {
                using (var cont = new SqlConnection(conn))
                {
                    var parameters = new DynamicParameters();

                    if (pageNumber > 1)
                        parameters.Add("@PageNbr", pageNumber);

                    if (pageSize > 10)
                        parameters.Add("@PageSize", pageSize);

                    if (!String.IsNullOrWhiteSpace(SortBy))
                        parameters.Add("@SortCol", SortBy);

                    result = cont.Query<ProductCategoryView>("sp_CategoryOfProduct_Search_Paging_Sorting", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at getProducts at ProductRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }

            return new Tuple<List<ProductCategoryView>, int>(result, totalItem);
        }

        public IEnumerable<v_CategoryOfProduct> GetCategoriesPaging(ProductCategoryFilter filter, int pageIndex = 1, int pageSize = 20, string sortby = "")
        {
            IQueryable<v_CategoryOfProduct> model = entities.v_CategoryOfProduct;
            try
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                {
                    string searchString = filter.SearchString.Trim();
                    model = model.Where(x => x.CategoryName.Contains(searchString));
                }

                if (filter.CatalogueId > 0)
                    model = model.Where(x => x.CatalogueId == filter.CatalogueId);

                if (filter.ParentId > 0)
                {
                    model = model.Where(x => x.CatalogueId == filter.ParentId);
                }

                if (!String.IsNullOrWhiteSpace(filter.Status))
                {
                    switch (filter.Status.Trim())
                    {
                        case nameof(StatusEntity.Active):
                            model = model.Where(w => w.Status == nameof(StatusEntity.Active));
                            break;
                        case nameof(StatusEntity.Locked):
                            model = model.Where(w => w.Status == nameof(StatusEntity.Locked));
                            break;
                        case nameof(StatusEntity.Deleted):
                        default:
                            model = model.Where(w => w.Status != nameof(StatusEntity.Deleted));
                            break;
                    }
                }

                if (!String.IsNullOrWhiteSpace(sortby))
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
                else
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetCategoriesPaging at ProductRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model.ToPagedList(pageIndex, pageSize);
        }

        public ProductCategoryView Create(ProductCategoryView model, out string message)
        {
            ProductCategoryView result = new ProductCategoryView();
            message = null;
            try
            {
                var productCategory = Helper.ConvertProductCategoryViewToProductCategory(model);
                productCategory.CreatedDate = DateTime.Now;
                productCategory.ModifiedDate = DateTime.Now;
                entities.ProductCategories.Add(productCategory);                
                if (entities.SaveChanges() > 0)
                {
                    result = GetProductCategoryById(productCategory.ID, out message);
                }
                else
                {
                    message = Resources.MSG_THE_PRODUCT_CATEGORY_HAS_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Create at ProductCategoryRepo at Model.Repository. ";
                message = StringHelper.Parameters2ErrorString(ex, model.ID);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }
        public ProductCategoryView Edit(ProductCategoryView model, out string message)
        {
            ProductCategoryView result = new ProductCategoryView();
            message = null;
            try
            {
                var productCategory = entities.ProductCategories.Find(model.ID);
                if (productCategory != null)
                {
                    productCategory.CatalogueId = model.CatalogueId;
                    productCategory.MetaTitle = model.MetaTitle;
                    productCategory.Name = model.Name;
                    productCategory.ParentID = model.ParentID;
                    productCategory.SeoTitle = model.SeoTitle;
                    productCategory.Status = model.Status;
                    productCategory.ShowOnHome = model.ShowOnHome;
                    productCategory.DisplayOrder = model.DisplayOrder;
                    //SEO
                    productCategory.MetaDescriptions = model.MetaDescriptions;
                    productCategory.MetaKeywords = model.MetaKeywords;
                    productCategory.ModifiedBy = model.ModifiedBy;
                    productCategory.ModifiedDate = DateTime.Now;

                    if (entities.SaveChanges() > 0)
                    {
                        result = GetProductCategoryById(productCategory.ID, out message);
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
                string subject = "Error " + SiteSetting.SiteName + " at Edit at ProductCategoryRepo at Model.Repository. ";
                message = StringHelper.Parameters2ErrorString(ex, model.ID);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }

    }
}
