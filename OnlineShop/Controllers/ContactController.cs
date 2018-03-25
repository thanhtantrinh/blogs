using Model.Dao;
using Model.EF;
using Model.ViewModel;
using OnlineShop.Helpers;
using OnlineShop.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace OnlineShop.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }

        [HttpGet]
        public ActionResult Inquiry()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inquiry(ContactModel model)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;
            string message = "";

            if (ModelState.IsValid)
            {                
                var result = new ContactDao().AddFeedback(model, out message);
                if (result!=null)
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, SiteResource.MSG_THE_CONTACT_HAS_BEEN_UPDATED);
                    actionStatus.ActionStatus = ResultSubmit.success;
                    Session["ACTION_STATUS"] = actionStatus;
                    //send email
                    string contentEmail = RenderRazorViewToString("EmailContactTemplate", result);
                    string subject = "Một khách hàng vừa gửi email hỗ trợ trên website";
                    MailHelper.SendMail(SiteConfiguration.EmailAdmin, "Admin", SiteConfiguration.EmailSite, SiteConfiguration.SiteName, subject, contentEmail);
                    return RedirectToAction("Index");
                }
                else
                {
                    actionStatus.ActionStatus = ResultSubmit.failed;
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, SiteResource.MSG_THE_CONTACT_HAS_NOT_BEEN_UPDATED);
                }

                Session["ACTION_STATUS"] = actionStatus;                
            }
            else
            {
                IsValid = false;
                foreach (var item in ModelState.Values)
                {
                    if (item.Errors.Count() > 0)
                    {
                        var errorItems = item.Errors.Where(f => !String.IsNullOrWhiteSpace(f.ErrorMessage)).ToList();
                        foreach (var erroritem in errorItems)
                        {
                            errorString += "<br />" + erroritem.ErrorMessage;
                        }
                    }
                }
                goto actionError;
            }

            actionError:
            if (!IsValid)
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_WARNING, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }            
            return View();
        }


        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.CreatedDate = DateTime.Now;
            feedback.Phone = mobile;
            feedback.Content = content;
            feedback.Address = address;

            var id = new ContactDao().InsertFeedBack(feedback);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
                //send mail
            }

            else
                return Json(new
                {
                    status = false
                });

        }


        public ActionResult EmailContactTemplate(bool send = false)
        {           
            var contactDao = new ContactDao();
            var model = contactDao.GetFeedback(3);
            if (send)
            {
                string contentEmail = RenderRazorViewToString("EmailContactTemplate", model);
                string subject = "Một khách hàng vừa gửi email hỗ trợ trên website";
                MailHelper.SendMail(SiteConfiguration.EmailAdmin, "Admin", SiteConfiguration.EmailSite, SiteConfiguration.SiteName, subject, contentEmail);
            }
            return View(model);
        }
    }
}