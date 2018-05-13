using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace Model.ViewModel
{
    public class OrderViewModel
    {
        public long ID { get; set; }
        public string FullName { get; set; }
        public long UserId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public List<OrderItem> Items {get; set;}

        public OrderViewModel()
        {
            Items = new List<OrderItem>();
        }

        public string OrderStatus
        {
            get
            {
                if (this.Status == nameof(eOrderStatusUI.Cancelled))
                    return "Đã hủy";
                else if (this.Status == nameof(eOrderStatusUI.Completed))
                    return "Đã giao";
                else
                    return "Đang chờ";
            }
        }
    }
    
    public class OrderItem
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }        
        public long ProductDetailId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public bool isProduct { get; set; }
        public OrderItem()
        {

        }
    }
}
