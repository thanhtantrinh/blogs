using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticResources;

namespace Model.ViewModel
{
    public class CatalogueView
    {
        public int Id { get; set; }
        public string CatalogueName { get; set; }

        [Display(Name = "Tên Website" )]
        [Required(ErrorMessage = "Bạn cần nhập thông tin tên website")]
        public string SiteName { get; set; }

        [Display(Name = "Tên miền Website")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin tên miền")]
        public string SiteUrl { get; set; }

        [Display(Name = "Email người quản trị")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin email nhân viên quản trị website")]
        public string EmailAdmin { get; set; }
        [Display(Name = "Email Website")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin email website. Email dùng gửi và nhận email khách hàng ")]
        public string EmailSite { get; set; }
        public long ManagerId { get; set; }
        public string Status { get; set; }

        [Display(Name = "Link Facebook")]
        public string Facebook { get; set; }

        [Display(Name = "Link Youtube")]
        public string Youtube { get; set; }

        [Display(Name = "Link Twitter")]
        public string Twitter { get; set; }

        [Display(Name = "Link GooglePlus")]
        public string GooglePlus { get; set; }

        [Display(Name = "Địa chỉ văn phòng ")]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại ")]
        public string Phones { get; set; }

        public long ModifiedById { get; set; }
        [Display(Name = "Ngày cập nhận lần cuối")]
        public DateTime ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Thẻ Meta Keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Thẻ Meta Descriptions")]
        public string MetaDescriptions { get; set; }
        [Display(Name = "Hỗ trợ bởi")]
        public string ManagerBy { get; set; }
        [Display(Name = "Được tạo bởi")]
        public string CreatedBy { get; set; }
        [Display(Name = "Cập nhận lần cuối ")]
        public string ModifiedBy { get; set; }
    }
}
