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
using AutoMapper;

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
        /// <summary>
        /// Get SiteMap from database
        /// </summary>
        /// <param name="catalogueId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static List<SiteMapItem> GetSiteMap(long catalogueId, out string message)
        {
            var result = new List<SiteMapItem>();
            message = null;
            try
            {
                using (var db = new SqlConnection(conn))
                {
                    var parameters = new DynamicParameters();
                    string SqlQuery = "SELECT * FROM v_SiteMap WHERE CatalogueId=@catalogueId";
                    if (catalogueId >= 0)
                    {
                        parameters.Add("@catalogueId", catalogueId);
                        result = db.Query<SiteMapItem>(SqlQuery, parameters).ToList();
                    }
                    else
                    {
                        message = String.Format(StaticResources.Resources.SYSTEM_ERROR_THE_PARAMETER_LARGE_ZERO, catalogueId);
                    }
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetSiteMap at Helper at Model.Extension";
                message += subject + StringHelper.Parameters2ErrorString(ex, catalogueId);

            }
            return result;
        }

        public static OrderModel ConvertCheckOutModelToOrder(CartModel checkOut)
        {
            OrderModel result = new OrderModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartModel, OrderModel>();
            });
            IMapper mapper = config.CreateMapper();
            result = mapper.Map<CartModel, OrderModel>(checkOut);

            return result;
        }
    }
}
