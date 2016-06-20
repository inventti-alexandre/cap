using Cap.Web.Common;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers
{
    [AreaAuthorizeAttribute("Erp", Roles = "configuracoes-r")]
    public class ConfiguracoesController : Controller
    {
        // GET: Erp/Configuracoes
        public ActionResult Index()
        {
            return View();
        }
    }
}