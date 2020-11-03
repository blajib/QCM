using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCMApp.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        public ActionResult Login(String error)
        {
            if (error != null)
            {
                ViewBag.Error = error;
            }
            return View();
        }
    }
}