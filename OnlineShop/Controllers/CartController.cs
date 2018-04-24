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
using Model;
using OnlineShop.Binders;

namespace OnlineShop.Controllers
{
    public class CartController : BaseController
    {
        [Route("gio-hang")]
        public ActionResult Index(CartModel cart)
        {
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = cart.CartItems;
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { status = true, result = RenderRazorViewToString("partialCart", list) });
            }
            return View(list);
        }

        public ActionResult ViewCart(CartModel cart)
        {
            var list = new List<CartItem>();
            if (cart != null && cart.CartItems.Count > 0)
            {
                list = cart.CartItems;
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { status = true, result = RenderRazorViewToString("partialViewCart", list) });
            }
            return View(list);
        }

        //public JsonResult DeleteAll()
        //{
        //    Session[CartSession] = null;
        //    return Json(new
        //    {
        //        status = true
        //    });
        //}

        public ActionResult Delete(CartModel cart, long Id = 0)
        {
            if (cart.CartItems.Count > 0 && Id > 0)
            {
                List<CartItem> Items = cart.CartItems;
                CartItem ItemDelete = Items.Where(w => w.ProductId == Id).FirstOrDefault();
                //int position = Items.IndexOf(ItemDelete);
                Items.Remove(ItemDelete);
                cart.CartItems = Items;
            }
            return RedirectToAction("Index");
        }

        //public JsonResult Update(CartModel cart, string cartModel)
        //{
        //    var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
        //    foreach (var item in cart.CartItems)
        //    {
        //        var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
        //        if (jsonItem != null)
        //        {
        //            item.Quantity = jsonItem.Quantity;
        //        }
        //    }
        //    Session[CartSession] = sessionCart;
        //    return Json(new { status = true, result = RenderRazorViewToString("partialCart", sessionCart) });
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CartModel cart, int[] quantity)
        {
            if (cart.CartItems.Count > 0)
            {
                int step = 0;
                List<CartItem> Items = cart.CartItems;
                foreach (var item in Items)
                {
                    if (quantity[step] > 0)
                    {
                        item.Quantity = quantity[step];
                    }
                    step++;
                }
                cart.CartItems = Items;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddItem(CartModel cart, long ID, int quantity = 1)
        {
            var product = new ProductDao().ViewDetail(ID);
            if (cart.CartItems.Exists(x => x.ProductId == ID))
            {
                foreach (var item in cart.CartItems)
                {
                    if (item.ProductId == ID)
                    {
                        item.Quantity += quantity;
                        item.isProduct = true;
                    }
                }
            }
            else
            {
                var item = new CartItem();
                item.ProductId = product.Id;
                item.ProductName = product.Name;
                item.ProductAlias = product.MetaTitle;
                item.ProductImage = product.Image;
                item.Quantity = quantity;
                item.Price = product.Price;
                cart.CartItems.Add(item);
            }
            return RedirectToAction("Detail", "Product", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Payment(CartModel cart)
        {
            CheckoutModel model = new CheckoutModel();
            model.CartItems = cart.CartItems;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(CartModel cart, CheckoutModel checkout)
        {
            string message = null;
            if (ModelState.IsValid)
            {
                if (cart.CartItems.Count()>0)
                {
                    OrderModel orderModel = Helper.ConvertCheckOutModelToOrder(checkout);
                    orderModel.Items = Helper.ConvertCartItemsToOrderItems(cart.CartItems);
                    orderModel.CreatedBy = 0;

                    orderModel = _orderRepo.CreateOrder(orderModel, out message);
                    if (String.IsNullOrWhiteSpace(message)&& orderModel != null)
                    {
                        return RedirectToAction("OrderConfirmation", new { ordernumber = orderModel.OrderId, send = true });
                    }                        
                }
                else
                {
                    return RedirectToAction("Payment");
                }
            }
            return View(checkout);
        }

        public async Task<ActionResult> OrderConfirmation(long ordernumber = 0, bool send = false)
        {
            if (ordernumber > 0)
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
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Success()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ListDistricts(int id = 0)
        {
            try
            {
                var address = new AddressDao();
                var districts = address.getDistricts(id).Select(s => new SelectListItem() { Value = s.Id.ToString(), Text = s.Name }).ToList();
                return Json(new { status = true, result = districts }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //send mail to site's manager
                throw;
            }

        }
    }
}