using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
    public class OrderModel
    {
        public long OrderId { get; set; }
        public string FullName { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedByName { get; set; }
        public long ModifiedBy { get; set; }

        public List<OrderItem> Items { get; set; }

        public OrderModel()
        {
            Items = new List<OrderItem>();
        }
    }

    public class OrderItem
    {
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }

        public OrderItem()
        {

        }
    }
}
