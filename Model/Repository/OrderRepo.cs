﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Model.ViewModel;
using Model.EF;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Dapper.Tvp;
using StaticResources;

namespace Model.Repository
{
    public class OrderRepo : BaseRepository
    {
        private string _conn;

        public OrderRepo() : base()
        {
            _conn = conn;   
        }

        public OrderRepo(string strConn)
        {
            _conn = strConn;
        }

        public OrderModel GetOrderById(long OrderId, out string message)
        {
            OrderModel result = new OrderModel();
            message = null;
            try
            {
                if (OrderId > 0)
                {
                    string sql = "SELECT * FROM v_Order WHERE OrderId = @OrderNumber; SELECT * FROM v_OrderDetail WHERE OrderId = @OrderNumber;";
                    var parameters = new DynamicParameters();
                    parameters.Add("@OrderNumber", OrderId);
                    using (var db = new SqlConnection(conn))
                    {
                        using (var multi = db.QueryMultiple(sql, parameters))
                        {
                            result = multi.Read<OrderModel>().FirstOrDefault();
                            result.Items = multi.Read<OrderItem>().ToList();
                            if (result == null)
                            {
                                message = Resources.MSG_ORDER_HAS_NOT_FOUND;
                            }
                            else
                            {
                                return result;
                            }
                        }
                    }
                }
                else
                {
                    message = message = String.Format(Resources.SYSTEM_ERROR_THE_PARAMETER_LARGE_ZERO, "OrderId");
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetOrderById at OrderRepo at Model.Repository. ";
                message = StringHelper.Parameters2ErrorString(ex, OrderId);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return null;
        }
        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public OrderModel CreateOrder(OrderModel model, out string message)
        {           
            message = null;
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

                    using (var db = new SqlConnection(conn))
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
                    if (orderNumber > 0)
                    {
                        return GetOrderById(orderNumber, out message);
                    }
                    else
                    {
                        message = Resources.MSG_ORDER_HAS_CREATED_UNSUCCESSFULLY;
                    }
                }
                else
                {
                    message = String.Format(Resources.SYSTEM_ERROR_THE_PARAMETER_LARGE_ZERO, "model");
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at CreateOrder at OrderRepo at  Model.Repository ";
                message = StringHelper.Parameters2ErrorString(ex);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return null;
        }
    }
}
