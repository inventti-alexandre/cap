using System.Web.Mvc;

namespace Cap.Web.Areas.Erp
{
    public class ModalInfoController : Controller
    {
        // GET: Erp/ModalInfo
        [ValidateInput(false)]
        public ActionResult Index(string message)
        {
            return PartialView(message);
        }
    }
}