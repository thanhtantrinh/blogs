using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }

    public class OrderView
    {
        public Payment payment { get; set; }
        public Shipping shipping { get; set; }
        public List<CartItem> carts { get; set; }
    }

    public class Payment
    {
        public string PaymentName { get; set; }
        public string PaymentType { get; set; }
        public Payment()
        {

        }
    }

    public class Shipping
    {
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string MobiPhone { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }

}