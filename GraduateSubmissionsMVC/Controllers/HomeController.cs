using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraduateSubmissionsMVC.Controllers
{
	
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole("Sys Admin"))
            {
                return RedirectToAction("Index", "SysAdmin");
            }
            else if (Request.IsAuthenticated && User.IsInRole("Reviewer"))
            {
                return RedirectToAction("Index", "Reviewer");
            }
            else
                return View();
        }

        public ActionResult DashBoard()
        {
            return View();
        }
    }
}
