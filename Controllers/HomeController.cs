using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcApplication40.Models;

namespace MvcApplication40.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
             return View(new DataLayer().GetCompleteBlogDetails());
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users U,string ReturnUrl="")
        {
          //  if (new DataLayer().Login(U))
            if(Membership.ValidateUser(U.UserName,U.Password))
            {
                FormsAuthentication.SetAuthCookie(U.UserName, true);
                Session["MyMenu"] = null;
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("MyProfile", "Home");
                }
            }
            ModelState.AddModelError("CustomError", "Invalid Login ID And Password");
            ModelState.Remove("Password");
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["MyMenu"] = null;
            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult MyProfile()
        {
            return View(new DataLayer().GetCompleteBlogDetailsByAuthor(HttpContext.User.Identity.Name));
        }
        public ActionResult GetBloggersList()
        {
                 return View(new DataLayer().GetBlogs());
        }
    }
}
