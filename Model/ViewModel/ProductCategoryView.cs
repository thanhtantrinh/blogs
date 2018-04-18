using StaticResources;
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
        public long ParentID { get; set; }

        [Display(Name = "Nhóm website")]
        [Required(ErrorMessage = "Chọn nhóm website")]
        public int CatalogueId { get; set; }

        [Display(Name = "LABEL_CATALOGUE_NAME", ResourceType = typeof(Resources))]
        [Required(ErrorMessage = "Chọn tên nhóm website")]
        public string CatalogueName { get; set; }

        [Display(Name = "Tên nhóm cha")]
        public string ParentName { get; set; }

        [Display(Name = "Thứ tự")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "SEO Title")]
        [StringLength(250)]
        public string SeoTitle { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Tạo bởi Id")]
        public long CreatedBy { get; set; }

        [StringLength(50)]
        [Display(Name = "Tạo bởi")]
        public string CreatedByName { get; set; }

        [Display(Name = "LABEL_MODIFIED_DATE", ResourceType = typeof(Resources))]
        public DateTime ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "LABEL_MODIFIED_BY", ResourceType = typeof(Resources))]
        public long ModifiedBy { get; set; }

        [StringLength(50)]
        [Display(Name = "LABEL_MODIFIED_NAME", ResourceType = typeof(Resources))]
        public string ModifiedByName { get; set; }

        [StringLength(250)]
        [Display(Name = "LABEL_KEYWORDS", ResourceType = typeof(Resources))]
        public string MetaKeywords { get; set; }

        [Display(Name = "Meta Descriptions")]
        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Hiện thị")]
        public string Status { get; set; }

        [Display(Name = "Trên trang chủ")]
        public bool ShowOnHome { get; set; }

        public int TotalCount { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }
    }

    public class ProductCategoryFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string SearchBy { get; set; }
        public string SearchString { get; set; }
        public int CatalogueId { get; set; }
        public int ParentId { get; set; }
        public string Status { get; set; }

        public ProductCategoryFilter()
        {
            DateFrom = new DateTime(DateTime.Now.Year, 01, 01);
            DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            SearchString = "";
            Status = "";
            CatalogueId = 0;
            ParentId = 0;
        }
    }


}
