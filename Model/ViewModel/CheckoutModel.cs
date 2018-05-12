using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CheckoutModel
    {
        //public List<CartItem> CartItems { get; set; }
        public Shipping ShippingDetail { get; set; }
        public Payment PaymentDetail { get; set; }

        public CheckoutModel()
        {
            ShippingDetail = new Shipping();
            PaymentDetail = new Payment();
           // CartItems = new List<CartItem>();
        }

    }
}
