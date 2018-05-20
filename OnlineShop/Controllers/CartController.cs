using Model.Dao;
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

        public ActionResult Delete(CartModel cart, long Id = 0, long ProductDetailId = 0)
        {
            if (cart.CartItems.Count > 0 && Id > 0 && ProductDetailId>0)
            {
                List<CartItem> Items = cart.CartItems;
                CartItem ItemDelete = Items.Where(w => w.ProductId == Id && w.ProductDetailId== ProductDetailId).FirstOrDefault();
                //int position = Items.IndexOf(ItemDelete);
                Items.Remove(ItemDelete);
                cart.CartItems = Items;
            }
            cart.DoPromotion();
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
                List<CartItem> Items = cart.CartItems.Where(w=>w.isProduct==true).ToList();
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
            cart.DoPromotion();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddItem(CartModel cart, long ID, int quantity = 1, long ProductDetailId = 0)
        {
            var product = new ProductDao().Find(ID, ProductDetailId);
            if (cart.CartItems.Exists(x => x.ProductId == ID && x.ProductDetailId== ProductDetailId))
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
                item.ProductId = product.ID;
                var productdetail = product.ProductDetail.FirstOrDefault(w => w.ProductDetailId == ProductDetailId);
                string strWeight = "";
                if (productdetail != null)
                {
                    var productWeight = productdetail.ProductWeight;
                    if (productWeight >= 1000)
                    {
                        strWeight = (productWeight / 1000).ToString() + " Kg";
                    }
                    else
                    {
                        strWeight = productWeight.ToString() + " G";
                    }
                }
                item.ProductName = String.Format(product.Name+" {0}", strWeight);
                item.ProductAlias = product.MetaTitle;
                item.ProductImage = product.Image;
                item.Quantity = quantity;
                item.Price = product.ProductPrice;
                item.ProductDetailId = ProductDetailId;
                cart.CartItems.Add(item);
            }
            cart.DoPromotion();
            return RedirectToAction("Detail", "Product", new { ID = ID, ProductDetailId= ProductDetailId });
        }

        [HttpGet]
        public ActionResult Payment(CartModel cart)
        {
            if (cart != null)
            {
                CheckoutModel model = new CheckoutModel();
                //model.CartItems = cart.CartItems;
                return View(model);
            }
            return RedirectToAction("Index","Home");
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
                    orderModel.CatalogueId = SiteConfiguration.CatalogueId;
                    orderModel = _orderRepo.CreateOrder(orderModel, out message);
                    if (String.IsNullOrWhiteSpace(message)&& orderModel != null)
                    {
                        cart.ClearCart();
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
                    return RedirectToAction("OrderConfirmation", new { ordernumber = ordernumber });
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


         public ActionResult PartialViewCartCheckOut(CartModel cart)
        {
            return View("_PartialViewCartCheckOut", cart.CartItems);
        }
    }
}