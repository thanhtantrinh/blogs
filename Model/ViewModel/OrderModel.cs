using Common;
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
        public int CatalogueId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
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

    public class OrderFilter
    {
        /// <summary>
        /// Order Number
        /// </summary>
        public string SearchString { get; set; }
        /// <summary>
        /// Catalogue Id
        /// </summary>
        public int CatalogueId { get; set; }
        /// <summary>
        /// Status of order
        /// </summary>
        public string[] Status { get; set; }

        public OrderFilter()
        {
            SearchString = "";
            Status = new string[] { nameof(eOrderStatusUI.Pending), nameof(eOrderStatusUI.Completed) };
            CatalogueId = 0;          
        }
    }

}
