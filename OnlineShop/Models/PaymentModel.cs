using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Models
{
    public class PaymentViewModel
    {
        public List<CartItem> cartItems { get; set; }
        public Shipping shippingdetail { get; set; }
        public Payment paymentdetail { get; set; }
        public SelectListItem[] provinces { get; set; }

        public PaymentViewModel()
        {
            var addressDB = new AddressDao();
            this.provinces = addressDB.getProvinces().Select(s=>new SelectListItem() { Value = s.Id.ToString(), Text = s.Name }).ToArray();
        }
    }
}