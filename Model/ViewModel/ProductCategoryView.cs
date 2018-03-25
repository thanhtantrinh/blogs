using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ProductCategoryView
    {
        public long ID { get; set; }
        [Display(Name = "Nhóm sản phẩm")]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Alias")]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Display(Name = "Nhóm cha")]
        public long? ParentID { get; set; }

        [Display(Name = "Tên nhóm cha")]
        public string ParentName { get; set; }

        [Display(Name = "Thứ tự")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "SEO Title")]
        [StringLength(250)]
        public string SeoTitle { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Tạo bởi Id")]
        public long CreatedBy { get; set; }

        [StringLength(50)]
        [Display(Name = "Tạo bởi")]
        public string CreatedByName { get; set; }

        [Display(Name = "Cập nhật")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Tạo bởi")]
        public long ModifiedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Keywords")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Meta Descriptions")]
        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Hiện thị")]
        public bool? Status { get; set; }

        [Display(Name = "Trên trang chủ")]
        public bool? ShowOnHome { get; set; }

        public int TotalCount { get; set; }
    }

    public class ProductCategoryFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string SearchBy { get; set; }        
        public string SearchString { get; set; }

        public ProductCategoryFilter()
        {
            DateFrom = new DateTime(DateTime.Now.Year, 01, 01);
            DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);            
            SearchString = "name";
        }
    }

    
}
