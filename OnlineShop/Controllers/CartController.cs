﻿using Model.Dao;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model.EF;
using Model.ViewModel;
using Common;
using System.Configuration;
using System.IO;
using OnlineShop.Helpers;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    
    public class CartController : BaseController
    {
        private const string CartSession = "CartSession";

        [Route("gio-hang")]
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { status = true, result = RenderRazorViewToString("partialCart", list) });
            }
            return View(list);
        }

        public ActionResult ViewCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { status = true, result = RenderRazorViewToString("partialViewCart", list) });
            }
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new { status = true, result = RenderRazorViewToString("partialCart", sessionCart) });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new { status = true, result = RenderRazorViewToString("partialCart", sessionCart) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int[] quantity)
        {            
            var sessionCart = (List<CartItem>)Session[CartSession];
            int step = 0;
            foreach (var item in sessionCart)
            {
                item.Quantity = quantity[step];
                step++;
            }
            Session[CartSession] = sessionCart;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddItem(long ID, int quantity = 1)
        {
            var product = new ProductDao().ViewDetail(ID);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == ID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == ID)
                        {
                            item.Quantity += quantity;
                            item.isProduct = true;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;                
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Detail", "Product", new { ID = ID });
        }
        
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            PaymentViewModel model = new PaymentViewModel();
            var cartItems = new List<CartItem>();
            if (cart != null)
            {
                cartItems = (List<CartItem>)cart;
                model.cartItems = cartItems;
                model.shippingdetail.ProvinceId = 79;
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ListDistricts(int id=0)
        {
            try
            {
                var address = new AddressDao();
                var districts = address.getDistricts(id).Select(s=>new SelectListItem() { Value = s.Id.ToString(), Text = s.Name}).ToList();
                return Json(new { status = true, result = districts }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //send mail to site's manager
                throw;
            }

        }
        
        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {

            try
            {
                var order = new Order();
                order.CreatedDate = DateTime.Now;
                order.ShipAddress = model.shippingdetail.Address;
                order.ShipMobile = model.shippingdetail.Phone;
                order.ShipName = model.shippingdetail.Fullname;
                order.ShipEmail = model.shippingdetail.Email;
                order.Status = nameof(eOrderStatusUI.Pending);
                order.Note = model.shippingdetail.Note;

                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price.Value;
                    orderDetail.Quantity = item.Quantity;                    
                    detailDao.Insert(orderDetail);                    
                }

                if (id > 0)
                {
                    return RedirectToAction("OrderConfirmation", new { ordernumber = id, send = true });
                }

            }
            catch (Exception ex)
            {
                //ghi log
                return RedirectToAction("Payment");
            }

            return Redirect("Payment");
        }
        public async Task<ActionResult> OrderConfirmation(long ordernumber=23, bool send = false)
        {
            var orderDao = new OrderDao();
            OrderViewModel model = orderDao.getOrderById(ordernumber);

            if (model != null && send)
            {
                string content = RenderRazorViewToString("OrderConfirmation", model);
                //MailHelper.SendMail(model.Email, "Thông tin xát nhận đơn hàng từ " + SiteConfiguration.SiteName, content);
                var bcc = new string[] { SiteConfiguration.EmailAdmin, "thanhtantrinh@hotmail.com" };
                var task = MailHelper.SendMailAsync(model.Email, model.FullName, SiteConfiguration.EmailSite, SiteConfiguration.SiteName, "Thông tin xác nhận đơn hàng từ " + SiteConfiguration.SiteName, content, null, bcc);

                await Task.WhenAll(task);
            }

            return View(model);
        }
        public ActionResult Success()
        {
            return View();
        }
        
    }
}