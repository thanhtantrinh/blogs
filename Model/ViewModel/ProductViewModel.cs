using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.ComponentModel.DataAnnotations;
using System.Web;

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

    public class ProductsView
    {        
        [Required]
        public long ID { set; get; }
        //[Required, FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        [Display(Name = "Hình sản phẩm")]
        public HttpPostedFileBase Images { get; set; }

        public string Image { get; set; }

        [Display(Name = "Hình chi tiết")]
        public string MoreImages { set; get; }

        [Display(Name = "Mã sản phẩm")]
        public string Code { set; get; }

        [Display(Name = "Tên Sản phẩm")]
        public string Name { set; get; }

        [Display(Name = "Giá")]        
        public decimal? Price { set; get; }

        [Display(Name = "Giá khuyến mãi")]
        public decimal? PromotionPrice { set; get; }

        [Display(Name = "Đã VAT")]
        public bool? IncludedVAT { set; get; }

        [Display(Name = "Nhóm Sản phẩm")]
        public long? CategoryID { get; set; }

        [Display(Name = "Nhóm Sản phẩm")]
        public string CatName { set; get; }

        public string CatMetaTitle { set; get; }

        [Display(Name = "Alias")]
        public string MetaTitle { set; get; }

        [Display(Name = "Còn hàng")]
        public int Quantity { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [Display(Name = "Mô tả ngắn")]
        public string Description { get; set; }

        [Display(Name = "Mô tả Sản phẩm")]
        public string Detail { get; set; }
                
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { set; get; }
        
        public string MetaKeywords { get; set; }
        public int? Warranty { get; set; }
                
        public string MetaDescriptions { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Trên trang chủ")]
        public bool? ShowHome { get; set; }

        public bool IsDiscount { get; set; }

    }
        

    public class MenuView
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public long ID { get; set; }
        public string Target { get; set; }
        public string Alias { get; set; }
    }
}
