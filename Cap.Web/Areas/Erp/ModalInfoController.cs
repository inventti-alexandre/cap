using System.Web.Mvc;

namespace Cap.Web.Areas.Erp
{
    public class ModalInfoController : Controller
    {
        // GET: Erp/ModalInfo
        [ValidateInput(false)]
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            return PartialView();
        }
    }
}