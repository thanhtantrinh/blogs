using Common;
using cPanel.Filters;
using cPanel.Resource;
using Model;
using Model.ViewModel;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace cPanel.Controllers
{
    [AuthLog(Roles = UserRoles.Admin)]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            if (CurrentUser != null && CurrentUser.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;
            string errorString = "";
            bool IsValid = true;

            if (ModelState.IsValid)
            {
                //var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password),true);
                if (!String.IsNullOrWhiteSpace(WebAccount.GetPassword(model.UserName)))
                {
                    UserAccount userAccount = new UserAccount();
                    userAccount.webAccount = WebAccount.load(model.UserName, Encryptor.MD5Hash(model.Password));

                    if (userAccount.webAccount != null)
                    {
                        if (userAccount.webAccount.Status == true)
                        {
                            userAccount.Roles = new string[] { userAccount.webAccount.GroupName };
                            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userAccount.webAccount.UserName, false);
                            authCookie.Expires = DateTime.Now.AddHours(24);//Expires in 1 day from today.
                            authCookie.HttpOnly = true; // protects from XSS attacks stealing cookies, makes the cookie hidden from Javascript (in proper browsers, IE6 doesn't support it).
                                                        //if (!Request.IsLocal)
                            authCookie.Secure = false;//FormsAuthentication.RequireSSL;                    
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, model.RememberMe, userAccount.UserDataString);
                            // Update the authCookie's Value to use the encrypted version of newTicket
                            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                            // Manually add the authCookie to the Cookies collection
                            Response.Cookies.Add(authCookie);

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
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
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, SiteResource.MSG_ERROR_ENTER_DATA_FOR_FORM + errorString);
                Session["ACTION_STATUS"] = actionStatus;
            }
            return View("Index");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}