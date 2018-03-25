using Common;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace Model.Repository
{
    public class CategoryRepo: BaseRepository
    {
        
        public CategoryRepo() : base(conn)
        {

        }

        public List<CategoryView> GetCategoriesPaging(ProductCategoryFilter filter, int pageNumber = 1, int pageSize = 20, string SortBy = "")
        {
            List<CategoryView> result = new List<CategoryView>();            
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

                    result = cont.Query<CategoryView>("sp_Category_Search_Paging_Sorting", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetCategories at CategoryRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            } 
            return result;
        }
    }
}
