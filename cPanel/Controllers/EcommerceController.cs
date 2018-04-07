using Common;
using cPanel.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cPanel.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class EcommerceController : BaseController
    {
        // GET: Ecommerce
        public ActionResult Index()
        {
            return View();
        }
        #region Category of product manager
        public ActionResult CategoryProduct()
        {

            return View();
        }

        public ActionResult CategoryProductEdit()
        {

            return View();
        }

        public ActionResult CategoryProductCreate()
        {

            return View();
        }
        #endregion

        #region Product manager
        public ActionResult Product()
        {

            return View();
        }
        public ActionResult ProductEdit()
        {

            return View();
        }

        public ActionResult ProductCreate()
        {

            return View();
        }
        #endregion
    }
}