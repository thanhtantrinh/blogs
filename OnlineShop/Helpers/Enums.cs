using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Helpers
{
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
        Completed,
        Pending,
        Cancelled
    }

    public class ActionResultHelper
    {
        public ResultSubmit ActionStatus { get; set; }
        public string ErrorReason { get; set; }
        public ActionResultHelper()
        {
            this.ActionStatus = ResultSubmit.success;
            this.ErrorReason = "";
        }
    }
}