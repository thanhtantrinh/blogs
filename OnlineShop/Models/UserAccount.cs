using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using Common;

namespace OnlineShop.Models
{
    public class UserAccount
    {
        private string[] _roles;
        public WebAccount webAccount;
        public string[] Roles
        {
            get
            {
                return _roles == null ? new string[] { CommonConstants.MEMBER_GROUP } : _roles;
            }
            set
            {
                _roles = value;
            }
        }
        public string DisplayName
        {
            get
            {
                return String.IsNullOrEmpty(webAccount.Name) ? webAccount.Name : webAccount.UserName;                
            }
        }
        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public string UserDataString
        {
            get
            {
                return string.Concat(String.Join(";", Roles), "|", webAccount.UserId, "|", DisplayName);
            }
        }
    }

    public class UserLogin
    {
        public string UserName { get; set; }
        public string GroupID { get; set; }        
        public long UserID { get; set; }
    }
}