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
    }
}
