namespace Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public static class UserRoles
    {
        public const string Admin = "admin";
        public const string Manager = "manager";
        public const string User = "user";
    }

    public enum StatusList
    {
        SUSSECSS, RUNNING, STARTING, ERROR
    }
    public enum StatusEntity
    {
        /// <summary>
        /// Entity have not been activated
        /// </summary>
        [Display(Name = "Hoạt động")]
        Active = 1,
        /// <summary>
        /// Entity has been deleted 
        /// </summary>
        [Display(Name = "Xóa")]
        Deleted = 2,
        /// <summary>
        /// Locked out
        /// </summary>
        [Display(Name = "Chờ")]
        Locked = 3,
    }
    public enum StatusLogin
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Customer does not exist (email or username)
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// Customer not registered 
        /// </summary>
        NotRegistered = 6,
        /// <summary>
        /// Locked out
        /// </summary>
        LockedOut = 7,

        /// <summary>
        /// Account have not been assigned to group  
        /// </summary>
        CustomerNotInGroup = 8,
    }
    public enum ResultSubmit
    {
        failed = 0,
        success = 1
    }
    public enum eSubmitStatus
    {
        None = 0,
        Success = 1,
        Failure = 2,
    }
    public enum eOrderStatusUI
    {
        [Display(Name = "Đã giao hàng")]
        Completed,
        [Display(Name = "Đang chờ")]
        Pending,
        [Display(Name = "Đã Hủy")]
        Cancelled
    }

    public class ActionResultHelper
    {
        public ResultSubmit ActionStatus { get; set; }
        public string Message { get; set; }
        public string ErrorReason { get; set; }
        public List<string> ErrorStrings { get; set; }
        public ActionResultHelper()
        {
            this.ActionStatus = ResultSubmit.success;
            this.ErrorReason = "";
            ErrorStrings = new List<string>();
        }
        public string ShowErrorStrings()
        {
            string errors = "";
            if (ErrorStrings.Count > 0)
            {
                errors += "<ul class='list-unstyled'>";
                foreach (var item in ErrorStrings)
                {
                    errors += "<li>" + item + "</li>";
                }
                errors += "</ul>";
            }
            return errors;
        }
    }
    public enum SitemapFrequency
    {
        Never,
        Yearly,
        Monthly,
        Weekly,
        Daily,
        Hourly,
        Always
    }
}
