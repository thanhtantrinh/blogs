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
    }
}
