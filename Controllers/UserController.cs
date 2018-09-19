using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication40.Models;

namespace MvcApplication40.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View(new DataLayer().UsersList());
        }
        public ActionResult EditUser(int id)
        {
            return View(new DataLayer().UsersList(id).Single(x=>x.ID==id));
        }
        [HttpPost]
        public ActionResult EditUser(Users U)
        {
            new DataLayer().EditUser(U);
            return View();
        }
        public ActionResult DeleteUser(int id)
        {
            return View(new DataLayer().UsersList(id).Single(x => x.ID == id));
        }
        [HttpPost]
        public ActionResult DeleteUser(Users U)
        {
            new DataLayer().DeleteUser(U);
            return View();
        }
        public ActionResult DetailsUSer(int id)
        {
            return View(new DataLayer().UsersList(id).Single(x => x.ID == id));
        }

    }
}
