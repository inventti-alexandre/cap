using Cap.Domain.Abstract.Admin;
using Cap.Web.Areas.Erp.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace Cap.Web.Areas.Erp.Controllers
{
    public class LoginController : Controller
    {
        private ILogin service;

        public LoginController(ILogin service)
        {
            this.service = service;
        }

        // GET: Erp/Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginUsuario());
        }

        [HttpPost]
        public ActionResult Index(LoginUsuario loginUsuario, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var usuario = service.ValidaLogin(loginUsuario.Login, loginUsuario.Senha);

                if (usuario != null)
                {
                    FormsAuthentication.SetAuthCookie(usuario.Login, false);
                    Session["IdUsuario"] = usuario.Id;
                    if (Url.IsLocalUrl(returnUrl)
                        && returnUrl.Length > 1
                        && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//")
                        && !returnUrl.StartsWith(@"\//"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário inválido");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(loginUsuario);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}