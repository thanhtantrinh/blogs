//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class v_Order
    {
        public long OrderId { get; set; }
        public System.Guid OrderNumber { get; set; }
        public int CatalogueId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string OrderStatus { get; set; }
        public string Note { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string Status { get; set; }
    }
}
