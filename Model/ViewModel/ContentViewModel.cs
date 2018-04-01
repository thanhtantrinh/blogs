using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModel
{
    public class ContentViewModel
    {
        public long ID { get; set; }
        [Display(Name = "Tên bài viết")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin")]
        public string Name { get; set; }
        [Display(Name = "Tên Alias")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin")]
        public string MetaTitle { get; set; }
        [Display(Name = "Tên Danh mục")]
        [Required(ErrorMessage = "Bạn chọn thông tin cho danh mục bài viết")]
        public long CategoryID { get; set; }
        [Display(Name = "Tên nhóm bài viết")]        
        public string CategoryName { get; set; }
        public string CategoryAlias { get; set; }
        [Display(Name = "Giới thiệu ngắn")]
        public string Description { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        [Display(Name = "Nội dung bài viết")]
        public string Detail { get; set; }
        [Display(Name = "Thứ tự")]
        public int Warranty { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Người tạo")]
        public long CreatedBy { get; set; }
        [Display(Name = "Tên người tạo")]
        public string CreatedByName { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime ModifiedDate { get; set; }
        [Display(Name = "Người cập nhật")]
        public long ModifiedBy { get; set; }
        [Display(Name = "Tên người cập nhật")]
        public string ModifiedByName { get; set; }
        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Meta Descriptions")]
        public string MetaDescriptions { get; set; }
        [Display(Name = "Xuất bản")]
        public string Status { get; set; }
        [Display(Name = "Lược xem")]
        public int ViewCount { get; set; }
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        [Display(Name = "Ngôn ngữ")]
        public string Language { get; set; }
    }

    public class ContentFilter
    {
        public long CategoryID { get; set; }
        public long CatalogueId { get; set; }
        public string SearchString { get; set; }
        public string Status { get; set; }

        public ContentFilter()
        {
            SearchString = "";
            Status = "";
            CategoryID = 0;
            CatalogueId = 0;
        }
    }

}
