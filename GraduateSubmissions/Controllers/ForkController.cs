using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraduateSubmissions.Controllers
{
    public class ForkController : Controller
    {
        //
        // GET: /Fork/

        public ActionResult Index()
        {
            ViewData["JoMomma"] = "Very false";
            return View();
        }

    }
}
