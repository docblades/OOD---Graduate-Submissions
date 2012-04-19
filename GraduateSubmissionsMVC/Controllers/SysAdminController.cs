using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraduateSubmissionsMVC.Controllers
{
    public class SysAdminController : Controller
    {
        //
        // GET: /SysAdmin/

		[Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
