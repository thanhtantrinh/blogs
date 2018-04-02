using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.EF;
using AutoMapper;
using Common;

namespace Model
{
    public class WebAccount
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }

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
        public string CreatedBy { get; set; }
        public long CreatedById { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public long ModifiedById { get; set; }

        public List<Role> roles { get; set; }

        public WebAccount()
        {

        }
        public static WebAccount load(string username, string password)
        {
            WebAccount userService = null;
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<v_WebAccount, WebAccount>();
                });
                IMapper mapper = config.CreateMapper();
                using (var db = new OnlineShopEntities())
                {
                    //DB.Query<WebAccount>
                    var result = db.v_WebAccount.Where(w => w.UserName == username && w.Password == password).FirstOrDefault();
                    userService = mapper.Map<WebAccount>(result);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at load at Model.Model";
                string message = StringHelper.Parameters2ErrorString(ex, username, password);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);                
            }

            return userService;
        }
        public static string GetPassword(string username)
        {
            string password = null;
            try
            {
                using (var db = new OnlineShopEntities())
                {
                    //DB.Query<WebAccount>
                    password = db.Users.Where(w => w.UserName == username).Select(s=>s.Password).FirstOrDefault();                    
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetPassword at Model.Model";
                string message = StringHelper.Parameters2ErrorString(ex, username);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return password;
        }


    }
}
