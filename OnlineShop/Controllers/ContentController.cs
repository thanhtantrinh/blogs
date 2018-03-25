using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using System.Web.Mvc;
using System.Configuration;
using Model.ViewModel;

namespace OnlineShop.Controllers
{
    public class ContentController : Controller
    {
        private ContentDao contentDao = new ContentDao();
        // GET: Content
        public ActionResult Index(int? page, long Id=0)
        {
            ContentFilter filter = new ContentFilter();
            filter.CategoryID = Id;
            var model = contentDao.ListAllPaging(filter, page ?? 1, 20);
            return View(model);
        }

        public ActionResult ContentProducts(long catid)
        {
            //var result = new ContentDao().ListContentByCatId(catid);
            var result = new CategoryDao().Products(catid);
            return View(result);
        }

        public ActionResult ContentProductDetail(long id)
        {
            var model = new ContentDao().GetByID(id);
            ViewBag.Tags = "";
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var model = new ContentDao().GetByID(id);
            ViewBag.Tags = "";
            return View(model);
        }

        public ActionResult Tag(string tagId, int page = 1, int pageSize = 10)
        {
            var model = new ContentDao().ListAllByTag(tagId, page, pageSize);
            int totalRecord = 0;
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Tag = new ContentDao().GetTag(tagId);
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, int productid, string company)
        {
            try
            {
                if (email != "" && mobile != "" && productid > 0)
                {
                    //send mail cho chi gai
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/customer_info.html"));

                    content = content.Replace("{{CustomerName}}", name);
                    content = content.Replace("{{Logo}}", this.GetBaseUrl() + "Images/logo.png");
                    content = content.Replace("{{BaseURL}}", this.GetBaseUrl());

                    content = content.Replace("{{Phone}}", mobile);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{Address}}", address);
                    content = content.Replace("{{Copyright}}", "Copyright © " + DateTime.Now.Year.ToString() + " BPT CHEMICALS CO.,LTD");
                    content = content.Replace("{{CompanyName}}", company);
                    content = content.Replace("{{CompanyMe}}", "BPT CHEMICALS");
                    content = content.Replace("{{CreateDate}}", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));


                    var product = new ContentDao().GetByID(productid);
                    //content = content.Replace("{{File}}", this.GetBaseUrl() + product.File);
                    content = content.Replace("{{ProductLink}}", this.GetBaseUrl() + "product/" + product.MetaTitle + "-" + product.ID);
                    content = content.Replace("{{ProductName}}", product.Name);


                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                    MailHelper.SendMail(toEmail, "Khách hàng muốn download file", content);
                    return Json(new { Status = true, Messeger = "Post Sucessful", Result = this.GetBaseUrl() });
                }
                else
                {
                    return Json(new { Status = false, Messeger = "Data enter is invalid", Result = "" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Messeger = ex.Message });
            }

        }
        public string GetBaseUrl()
        {
            var request = System.Web.HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;
            if (!string.IsNullOrWhiteSpace(appUrl)) appUrl += "";
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            return baseUrl;
        }
    }
}