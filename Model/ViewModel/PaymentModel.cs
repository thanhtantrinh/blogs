using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Model.ViewModel
{
    public class CartModel
    {
        public List<CartItem> CartItems { get; set; }
        public Shipping ShippingDetail { get; set; }
        public Payment PaymentDetail { get; set; }        

        public CartModel()
        {
            ShippingDetail = new Shipping();
            PaymentDetail = new Payment();
            CartItems = new List<CartItem>();
        }
    }
    public class CartItem
    {
        public long ProductId { get; set; }
        public string ProductName { set; get; }
        public string ProductAlias { set; get; }
        public string ProductImage { set; get; }
        public decimal Price { get; set; }
        public int Quantity { set; get; }
        public bool isProduct { get; set; }
        public CartItem()
        {
            isProduct = true;
            Quantity = 1;            
        }
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string MobiPhone { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }


}