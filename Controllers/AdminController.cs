using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication40.Models;
namespace MvcApplication40.Controllers
{
     [Authorize]
    public class AdminController : Controller
    {
          [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult UserIndex()
        {
           return View();
        }
         [Authorize]
        public ActionResult AddBlog()
        {
            return View();
        }
         [HttpPost]
         public string AddBlog(BlogTbl Blg)
         {
             Blg.CreatedBy = HttpContext.User.Identity.Name;
             if (new DataLayer().AddBlog(Blg)) { return string.Format("<script>alert('Blog Added');location.assign('/Home/MyProfile');</script>"); }
             else { return string.Format("<script>alert(Error Occured');location.assign('/Home/AddBlog');</script>"); }
             
         }
         [Authorize(Roles="Admin")]
         public ActionResult AddNewUser() 
         {
             return View(); 
         }
         [HttpPost]
         public string AddNewUser(Users U)
         {
             byte[] data = new byte[U.File.ContentLength];
             U.File.InputStream.Read(data,0,U.File.ContentLength);
             U.ImageData = data;
             if (new DataLayer().AddNewUser(U)) { return string.Format("<script>alert('User Added ');location.assign('/User/Index')</script>"); }
             else { return string.Format("<script>alert('User Not Added \n Some Error Occured ');location.assign(/Admin/AddNewUser);</script>"); }
         }
    }
}
