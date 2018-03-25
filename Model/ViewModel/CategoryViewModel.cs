using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{

    public class CategoryViewModel
    {
        public long ID { get; set; }        
        public string Name { get; set; }
        public string MetaTitle { get; set; }        
        public long ParentID { get; set; }        
        public string ParentName { get; set; }
        public int DisplayOrder { get; set; }
        public string SeoTitle { get; set; }        
        public DateTime CreatedDate { get; set; }        
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public bool Status { get; set; }
        public bool ShowOnHome { get; set; }
        public string Language { get; set; }
    }
    public class CategoryView
    {
        public long ID { get; set; }

        [Display(Name = "Tên nhóm bài viết")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin")]
        public string Name { get; set; }
        [Display(Name = "Tên Alias")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin")]
        public string MetaTitle { get; set; }
        [Display(Name = "Tên Nhóm Cha")]
        [Required(ErrorMessage = "Bạn chọn thông tin cho nhóm cha")]
        public long ParentID { get; set; }
        public string ParentCategoryName { get; set; }
        [Display(Name = "Thứ tự ưu tiên")]
        public int DisplayOrder { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        public string SeoTitle { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Người tạo")]
        public long CreatedBy { get; set; }
        [Display(Name = "Tên người tạo")]
        public string CreatedByName { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime ModifiedDate { get; set; }
        public long ModifiedBy { get; set; }
        [Display(Name = "Tên người cập nhật")]
        public string ModifiedByName { get; set; }
        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Meta Descriptions")]
        public string MetaDescriptions { get; set; }
        [Display(Name = "Xuất bản")]
        public bool Status { get; set; }
        [Display(Name = "Ngôn ngữ")]
        public string Language { get; set; }
        [Display(Name = "Hiện thị trang chủ")]
        public bool ShowOnHome { get; set; }
    }

    public class CategoryFilter
    {
        public long ParentID { get; set; }
        public string SearchString { get; set; }
        public string Status { get; set; }

        public CategoryFilter()
        {
            SearchString = "";
            Status = "";
            ParentID = 0;
        }
    }

}
