using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;

namespace Model.Dao
{
    public class OrderDao
    {
        OnlineShopEntities db = null;
        public OrderDao()
        {
            db = new OnlineShopEntities();
        }
        public long Insert(Order order)
        {
            order.CreatedDate = DateTime.Now;            
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        public OrderViewModel getOrderById(long orderId)
        {
            var result = new OrderViewModel();

            var order = db.Orders.Where(w => w.ID == orderId).FirstOrDefault();
            result.Address = order.ShipAddress;
            result.Email = order.ShipEmail;
            result.Phone = order.ShipMobile;
            result.FullName = order.ShipName;
            result.CreateDate = order.CreatedDate;
            result.ID = order.ID;
            result.Status = order.Status;
            result.Items = db.v_OrderDetail.Where(w => w.OrderID == orderId)
                .Select(s => new OrderItem() {
                    OrderId = s.OrderID,
                    Price = s.Price,
                    ProductId = s.ProductID,
                    Quantity = s.Quantity,
                    ProductName = s.ProductName,
                    ProductImage =s.ProductImage }).ToList();

            return result;
        }
    }
}
