﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OnlineShopEntities : DbContext
    {
        public OnlineShopEntities()
            : base("name=OnlineShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Catalogue> Catalogues { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContentTag> ContentTags { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<v_CatalogueInfo> v_CatalogueInfo { get; set; }
        public virtual DbSet<v_WebAccount> v_WebAccount { get; set; }
        public virtual DbSet<v_CategoryOfProduct> v_CategoryOfProduct { get; set; }
        public virtual DbSet<v_Category> v_Category { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<v_Content> v_Content { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
    
        public virtual ObjectResult<sp_Category_Search_Paging_Sorting_Result> sp_Category_Search_Paging_Sorting(Nullable<int> pageNbr, Nullable<int> pageSize, string sortCol)
        {
            var pageNbrParameter = pageNbr.HasValue ?
                new ObjectParameter("PageNbr", pageNbr) :
                new ObjectParameter("PageNbr", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColParameter = sortCol != null ?
                new ObjectParameter("SortCol", sortCol) :
                new ObjectParameter("SortCol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Category_Search_Paging_Sorting_Result>("sp_Category_Search_Paging_Sorting", pageNbrParameter, pageSizeParameter, sortColParameter);
        }
    
        public virtual ObjectResult<sp_CategoryOfProduct_Search_Paging_Sorting_Result> sp_CategoryOfProduct_Search_Paging_Sorting(Nullable<int> pageNbr, Nullable<int> pageSize, string sortCol)
        {
            var pageNbrParameter = pageNbr.HasValue ?
                new ObjectParameter("PageNbr", pageNbr) :
                new ObjectParameter("PageNbr", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColParameter = sortCol != null ?
                new ObjectParameter("SortCol", sortCol) :
                new ObjectParameter("SortCol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_CategoryOfProduct_Search_Paging_Sorting_Result>("sp_CategoryOfProduct_Search_Paging_Sorting", pageNbrParameter, pageSizeParameter, sortColParameter);
        }
    }
}
