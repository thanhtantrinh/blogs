using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;
using Model.Repository;
using Model.ViewModel;
using Dapper;

namespace TestList
{

   public class Program
    {
        public static OrderModel CreateOrder(OrderModel model)
        {

            try
            {
                long orderNumber = 0;

                if (model != null && model.Items.Count > 0)
                {
                    var dt = new DataTable();

                    dt.Columns.Add("ProductId", typeof(long));
                    dt.Columns.Add("ProductDetailId", typeof(long));
                    dt.Columns.Add("OrderId", typeof(long));
                    dt.Columns.Add("ProductName", typeof(string));
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("Price", typeof(decimal));

                    foreach (var item in model.Items)
                    {
                        dt.Rows.Add(item.ProductId, item.ProductDetailId, 0, item.ProductName, item.Quantity, item.Price);
                    }

                    using (var db = new SqlConnection("data source=TANTRINH\\SQLEXPRESS;initial catalog=tant4482_db;user id=sa;password=123456789; Pooling=True"))
                    {
                        var parameters = new DynamicParameters();

                        parameters.Add("@UserID", model.CreatedBy);
                        parameters.Add("@ShipName", model.FullName);
                        parameters.Add("@ShipMobile", model.Phone);
                        parameters.Add("@ShipAddress", model.Address);
                        parameters.Add("@ShipEmail", model.Email);
                        parameters.Add("@ShipStatus", nameof(eOrderStatusUI.Pending));
                        parameters.Add("@Notes", model.Notes);
                        parameters.Add("@OrderItems", dt.AsTableValuedParameter("dbo.UDT_CartItem"));
                        parameters.Add("@OrderNumber", DbType.Int32, direction: ParameterDirection.Output);
                        db.Query("sp_OrderCreate", parameters, commandType: CommandType.StoredProcedure);
                      
                        orderNumber = parameters.Get<int>("@OrderNumber");

                    }

                }

            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at CreateOrder at OrderRepo at  Model.Repository ";

            }
            return null;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("test send mail");
            //var task = MailHelper.SendMailAsync("thanhtan.hello@gmail.com", "To Testing", "info@tantrinh.vn", "tantrinh.vn", "testing email", "Testing email from console",null,null);
            string msg = "";                      

            var order = new OrderModel();

            order.Address = "Nguyen luong bang";
            order.Email = "thanhtan.hello@gmail.com";
            order.FullName = "Tan Trinh";
            order.Notes = "Note";
            order.Phone = "0987110341";
            order.Status = "Pending";
            order.Items = new List<OrderItem>() {
                new OrderItem(){OrderId=0,isProduct=true,ProductId=50,ProductDetailId=1,ProductName="CÁ THU 3 LÁT 500G",Quantity=1 },
                new OrderItem(){OrderId=0,isProduct=true,ProductId=50,ProductDetailId=2,ProductName="CÁ THU 3 LÁT 1000G",Quantity=1 }

            };
            //var t = new Test();

            order = CreateOrder(order);


            //try
            //{

            //    using (var db = new SqlConnection("data source=TANTRINH\\SQLEXPRESS;initial catalog=tant4482_db;user id=sa;password=123456789; Pooling=True"))
            //    {
            //        var parameters = new DynamicParameters();      
            //        parameters.Add("@OrderNumber", DbType.Int32, direction: ParameterDirection.Output);
            //        var result = db.Query<long>("testStore", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            //        var orderNumber = parameters.Get<int>("@OrderNumber");

            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

            Console.WriteLine("the end: " + msg);
            Console.ReadLine();
        }
    }
}
