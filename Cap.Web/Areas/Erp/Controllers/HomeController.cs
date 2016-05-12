using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers
{
    [AreaAuthorizeAttribute("Erp", Roles ="erp")]
    public class HomeController : Controller
    {
        // GET: Erp/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}