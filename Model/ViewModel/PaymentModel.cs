using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public long ProductDetailId { get; set; }
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
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Mời nhập họ và tên")]
        public string Fullname { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Mời nhập điện thoại")]
        public string Phone { get; set; }

        public string MobiPhone { get; set; }
        public string CompanyName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Mời nhập địa chỉ mail")]
        public string Email { get; set; }
        [Display(Name = "Tỉnh thành")]
        [Required(ErrorMessage = "Mời chọn tỉnh thành")]
        public int ProvinceId { get; set; }
        [Display(Name = "Quận Huyện")]
        [Required(ErrorMessage = "Mời chọn quận huyện")]
        public int DistrictId { get; set; }
        [Display(Name = "Xã, phường")]
        //[Required(ErrorMessage = "Mời nhập xã phường")]
        public int WardId { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Mời nhập địa chỉ của bạn")]
        public string Address { get; set; }

        [Display(Name = "Ghi chú thêm")]
        public string Note { get; set; }
    }
}