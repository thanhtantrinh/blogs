using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model.ViewModel;

namespace OnlineShop.Binders
{
    public class CartBinder : IModelBinder
    {
        //private string sessionKey = "session_cart";
        public object BindModel(ControllerContext ctx, ModelBindingContext binding_context)
        {
            CartModel cartInfo = (CartModel)ctx.HttpContext.Session[CommonConstants.CartSession];
            if (cartInfo == null)
            {
                cartInfo = new CartModel();
                ctx.HttpContext.Session[CommonConstants.CartSession] = cartInfo;
            }

            // cart.DoPromotions();
            return cartInfo;
        }
    }
}