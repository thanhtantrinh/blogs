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
    
    public partial class v_WebAccount
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<int> WardID { get; set; }
        public string WardName { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public string DistrictName { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public string Status { get; set; }
    }
}
