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
    
    public partial class v_ProductDetail
    {
        public long ProductDetailId { get; set; }
        public long ProductId { get; set; }
        public string ProductSize { get; set; }
        public double ProductWeight { get; set; }
        public double ProductPrice { get; set; }
        public Nullable<long> PriceId { get; set; }
        public Nullable<int> PriceTypeId { get; set; }
        public string PriceTypeName { get; set; }
    }
}
