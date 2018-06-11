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
    
    public partial class v_Product
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string ProductAlias { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string MoreImages { get; set; }
        public double ProductPrice { get; set; }
        public long ProductDetailId { get; set; }
        public string ProductSize { get; set; }
        public double ProductWeight { get; set; }
        public Nullable<bool> IncludedVAT { get; set; }
        public bool Quantity { get; set; }
        public int CatalogueId { get; set; }
        public string CatalogueName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryAlias { get; set; }
        public string Detail { get; set; }
        public Nullable<int> Warranty { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> TopHot { get; set; }
        public Nullable<int> ViewCount { get; set; }
        public bool ShowHome { get; set; }
        public bool New { get; set; }
        public int SortValue { get; set; }
    }
}
