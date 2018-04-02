using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Repository;
using Model.ViewModel;
using Dapper;
using System.Data.SqlClient;
using Common;

namespace Model
{
    public class Helper : BaseRepository
    {
        public Helper() : base()
        {

        }

        public static List<MenuItem> GetMenuByCatalogueId(long catalogueId, out string message)
        {                        
            var result = new List<MenuItem>();
            message = null;
            try
            {
                using (var db = new SqlConnection(conn))
                {
                    var parameters = new DynamicParameters();
                    string SqlQuery = "SELECT * FROM v_Menu WHERE CatalogueId=@catalogueId";
                    if (catalogueId >= 0)
                    {
                        parameters.Add("@catalogueId", catalogueId);
                        result = db.Query<MenuItem>(SqlQuery, parameters).ToList();
                    }
                    else
                    {
                        message = String.Format(StaticResources.Resources.SYSTEM_ERROR_THE_PARAMETER_LARGE_ZERO, catalogueId);
                    }
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetMenuByCatalogueId at Helper at Model.Extension";
                message += subject + StringHelper.Parameters2ErrorString(ex, catalogueId);

            }
            return result;
        }
    }
}
