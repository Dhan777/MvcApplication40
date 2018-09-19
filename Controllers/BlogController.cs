using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication40.Models;
namespace MvcApplication40.Controllers
{
    public class BlogController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(string id)
        {
            return View(new DataLayer().GetBlogDetails(id));
        }
        public ActionResult ReadBlog(int id)
            {
           // return View(new DataLayer().ReadBlog(id).Single(x=>x.BlogID==id));
                return View(new DataLayer().GetCompleteBlogDetails(id).Single(x=>x.BlogID==id));
           }
        public ActionResult ReadBlogAccordingToTechnology(int id)
        {
            return View(new DataLayer().GetCompleteBlogDetailsByTechnology(id));
        }
        public ActionResult ReadBlogAccordingToAuthor(string id)
        {
            return View(new DataLayer().GetCompleteBlogDetailsByAuthor(id));
        }
        public ActionResult EditBlog(int id)
        {
            return View(new DataLayer().GetCompleteBlogDetails(id).Single(x => x.BlogID == id));
        }
        [HttpPost]
        public ActionResult EditBlog(BlogTbl B)
        {
            new DataLayer().EditBlog(B);
            return Redirect("/Home/MyProfile");
        }
        public ActionResult DeleteBlog(int id)
        {
            return View(new DataLayer().GetCompleteBlogDetails(id).Single(x => x.BlogID == id));
        }
        [HttpPost]
        public ActionResult DeleteBlog(BlogTbl B)
        {
                 new DataLayer().DeleteBlog(B);
                 return Redirect("/Home/MyProfile");
        }
    }
}
