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
        
    }
}
