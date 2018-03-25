using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogs.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Categories
        public ActionResult Index(int id)
        {
            return View();
        }
    }
}