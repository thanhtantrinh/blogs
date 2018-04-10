using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
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

}
