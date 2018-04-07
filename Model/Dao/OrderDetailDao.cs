using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        OnlineShopEntities db = null;
        public OrderDetailDao()
        {
            db = new OnlineShopEntities();
        }
        public long Insert(OrderDetail detail)
        {
            try
            {
                detail.CreateBy = 0;
                detail.CreateDate = DateTime.Now;
                detail.ModifiedBy = 0;
                detail.ModifiedDate = DateTime.Now;
                detail = db.OrderDetails.Add(detail);
                db.SaveChanges();

                return detail.OrderID;
            }
            catch
            {
                return 0;

            }
        }
    }
}
