using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication40.Models;

namespace MvcApplication40.Controllers
{
    public class TechnologyController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles="Admin")]
        public ActionResult AddTechnology()
        {
            return View();
        }
        [HttpPost]
        public string  AddTechnology(BlogTbl blg)
        {
            byte[] data = new byte[blg.File.ContentLength];
            blg.File.InputStream.Read(data, 0, blg.File.ContentLength);
            blg.ImageData = data;
            if (new DataLayer().AddTechnology(blg)) { return string.Format("<script>alert('Technology Added Successfully');location.assign('/Technology/BlogTechnologies')</script>"); }
            else { return string.Format("<script>alert('Technology Not Added \n Error Occured')'location.assign('/Technology/AddTechnology')</script>"); }
        }
        public ActionResult TechnologyList()
        {
           // return View(new DataLayer().TechnologiesList());
            var q = new DataLayer().TechnologiesList();
            return Json(q, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BlogTechnologies()
        {
            return View(new DataLayer().BlogTechnologies());
        }
    }
}
