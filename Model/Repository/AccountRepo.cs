using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class AccountRepo: BaseRepository
    {
        public AccountRepo() : base()
        {

        }

        #region Rule manager
        public IEnumerable<v_Role> GetRulesPaging(int pageNumber = 1, int pageSize = 20, string SortBy = "")
        {
            IQueryable<v_Role> model = entities.v_Role;

            try
            {
                if (!String.IsNullOrWhiteSpace(SortBy))
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
                else
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetRulesPaging at AccountRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }

            return model.ToPagedList(pageNumber, pageSize);
        }
        #endregion

        #region Rule manager
        public IEnumerable<v_UserGroup> GetUserGroupPaging(int pageNumber = 1, int pageSize = 20, string SortBy = "")
        {
            IQueryable<v_UserGroup> model = entities.v_UserGroup;

            try
            {
                if (!String.IsNullOrWhiteSpace(SortBy))
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
                else
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetRulesPaging at AccountRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }

            return model.ToPagedList(pageNumber, pageSize);
        }
        #endregion

        #region User manager
        public IEnumerable<v_WebAccount> GetUserPaging(int pageNumber = 1, int pageSize = 20, string SortBy = "")
        {
            IQueryable<v_WebAccount> model = entities.v_WebAccount;
            try
            {
                if (!String.IsNullOrWhiteSpace(SortBy))
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
                else
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetUserPaging at AccountRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }

            return model.ToPagedList(pageNumber, pageSize);
        }
        #endregion
    }
}
