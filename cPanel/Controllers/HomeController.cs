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
            LoginModel model = new LoginModel();
            if (CurrentUser != null && CurrentUser.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password),true);
                if (!String.IsNullOrWhiteSpace(WebAccount.GetPassword(model.UserName)))
                {
                    UserAccount userAccount = new UserAccount();
                    userAccount.webAccount = WebAccount.load(model.UserName, Encryptor.MD5Hash(model.Password));

                    if (userAccount.webAccount != null)
                    {
                        if (userAccount.webAccount.Status == nameof(StatusEntity.Active))
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
                        else if(userAccount.webAccount.Status == nameof(StatusEntity.Locked))
                        {                            
                            ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tài khoản đã xóa.");
                        }

                    }
                    else
                    {
                                             
                        ModelState.AddModelError("Password", "Mật khẩu không đúng.");
                    }

                }
                else
                {
                  
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        [OutputCache(Duration = 3600, VaryByCustom = "User")]
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