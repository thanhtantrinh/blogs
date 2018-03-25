using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModel
{
    public class ContactModel
    {
        public int ID { get; set; }
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin họ và tên")]
        public string Name { get; set; }
        public string Person { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin số điện thoại")]        
        //[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Bạn cần nhập vào số điện thoại.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Bạn cần nhập vào số điện thoại")]
        public string Phone { get; set; }

        public string MobilePhone { get; set; }
        [Display(Name = "Địa chỉ Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ email chưa đúng, vui lòng kiểm tra lại.")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin email để nhận phản hồi")]
        public string Email { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Nội dung cần hỗ trợ")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin cần hỗ trợ")]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
