using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class AccountModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int WardId { get; set; }
        public string WardName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedByName { get; set; }
        public long ModifiedBy { get; set; }
    }
    public class AccountFilter
    {               
        public string SearchString { get; set; }
        public int CatalogueId { get; set; }        
        public string[] Status { get; set; }

        public AccountFilter()
        {
            SearchString = "";
            Status = new string[] { nameof(StatusEntity.Active), nameof(StatusEntity.Locked) };
            CatalogueId = 0;            
        }
    }
}
