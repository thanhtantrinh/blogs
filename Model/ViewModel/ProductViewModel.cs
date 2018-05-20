using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.ComponentModel.DataAnnotations;
using System.Web;
using StaticResources;
using Common;
namespace Model.ViewModel
{
    public class ProductViewModel
    {
        public long ID { set; get; }
        public string Images { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public string CateName { set; get; }
        public string CateMetaTitle { set; get; }
        public string MetaTitle { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    public class ProductDetailModel
    {
        public long ProductDetailId { set; get; }
        [Display(Name = "Size")]
        public string ProductSize { get; set; }
        [Display(Name = "Trọng lượng")]
        public double ProductWeight { get; set; }
        [Display(Name = "Giá")]
        public double ProductPrice { get; set; }
        public long PriceId { get; set; }
        public int PriceTypeId { get; set; }
        public string PriceTypeName { get; set; }
        public string UnitOfWeight { get; set; }
        public ProductDetailModel()
        {
            ProductSize = "";
            ProductWeight = 0;
            ProductPrice = 0;
            PriceTypeId = 1;
            PriceId = 0;
            ProductDetailId = 0;
            UnitOfWeight = "gram";
        }
    }

    /// <summary>
    /// Product view to edit, create 
    /// </summary>
    public class ProductsView
    {
        [Required]
        public long ID { set; get; }

        //[Required, FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        [Display(Name = "Hình")]
        public HttpPostedFileBase Images { get; set; }

        public string Image { get; set; }

        [Display(Name = "Hình chi tiết")]
        public string MoreImages { set; get; }

        [Display(Name = "Mã sản phẩm")]
        public string Code { set; get; }

        [Display(Name = "Tên Sản phẩm")]
        public string Name { set; get; }
        [Display(Name = "Alias")]
        public string MetaTitle { set; get; }

        public long ProductDetailId { set; get; }
        /// <summary>
        /// price of the product
        /// </summary>
        [Display(Name = "Giá")]
        public decimal ProductPrice { set; get; }

        [Display(Name = "Giá khuyến mãi")]
        public decimal? PromotionPrice { set; get; }

        [Display(Name = "Đã VAT")]
        public bool? IncludedVAT { set; get; }
        
        [Display(Name = "Nhóm Sản phẩm")]
        public long CategoryID { get; set; }

        [Display(Name = "Tên Nhóm Sản phẩm")]
        public string CategoryName { set; get; }

        public string CategoryAlias { set; get; }
        [Display(Name = "Nhóm Catalogue")]
        public int CatalogueId { get; set; }

        [Display(Name = "Tên nhóm Catalogue")]
        public string CatalogueName { get; set; }

        [Display(Name = "Còn hàng")]
        public int Quantity { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [Display(Name = "Mô tả ngắn")]
        public string Description { get; set; }
        [Display(Name = "Mô tả Sản phẩm")]
        public string Detail { get; set; }

        [Display(Name = "LABEL_CREATED_BY", ResourceType = typeof(Resources))]
        public long CreatedBy { get; set; }
        [Display(Name = "LABEL_CREATED_NAME", ResourceType = typeof(Resources))]
        public string CreatedByName { get; set; }
        [Display(Name = "LABEL_CREATED_DATE", ResourceType = typeof(Resources))]
        public DateTime CreatedDate { set; get; }

        [Display(Name = "LABEL_MODIFIED_DATE", ResourceType = typeof(Resources))]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "LABEL_MODIFIED_BY", ResourceType = typeof(Resources))]
        public long ModifiedBy { get; set; }

        [Display(Name = "LABEL_MODIFIED_NAME", ResourceType = typeof(Resources))]
        public string ModifiedByName { get; set; }

        public string MetaKeywords { get; set; }
        public int? Warranty { get; set; }

        public string MetaDescriptions { get; set; }
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Trên trang chủ")]
        public bool? ShowHome { get; set; }

        public bool IsDiscount { get; set; }
        public string Language { get; set; }

        #region product detail
        public List<ProductDetailModel> ProductDetail { get; set; }
        #endregion
    }
    public class ProductFilter
    {
        public string SearchString { get; set; }
        public int CatalogueId { get; set; }
        /// <summary>
        /// Category of Product
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Status of product
        /// </summary>
        public string[] Status { get; set; }
        /// <summary>
        /// Products are new
        /// </summary>
        public bool? IsNew { get; set; }
        /// <summary>
        /// Show Home Page
        /// </summary>
        public bool? IsShowHome { get; set; }

        public ProductFilter()
        {
            SearchString = "";
            Status = new string[] { nameof(StatusEntity.Active), nameof(StatusEntity.Locked) };
            CatalogueId = 0;
            CategoryId = 0;
        }
    }
}
