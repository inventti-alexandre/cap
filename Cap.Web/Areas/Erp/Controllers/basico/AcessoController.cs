using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    public class AcessoController : Controller
    {
        private IUsuarioRegra service;
        private IBaseService<Usuario> serviceUsuario;

        public AcessoController(IUsuarioRegra service, IBaseService<Usuario> serviceUsuario)
        {
            this.service = service;
            this.serviceUsuario = serviceUsuario;
        }

        // GET: Erp/Acesso
        public ActionResult Index(int id)
        {
            ViewBag.IdUsuario = id;
            ViewBag.Nome = serviceUsuario.Find(id).Nome;

            return View(service.GetRegras(id).ToList().OrderBy(x => x.SistemaTela.Descricao));
        }

        // POST: Erp/Acesso
        [HttpPost]
        public ActionResult Index(int idUsuario, string[] selecionado)
        {
            int[] idTelas = new int[selecionado.Length];
            int[] idRegras = new int[selecionado.Length];

            for (int i = 0; i < selecionado.Length; i++)
            {
                var valor = selecionado[i].Split('|');
                idTelas[i] = Convert.ToInt32(valor[0]);
                idRegras[i] = Convert.ToInt32(valor[1]);
            }

            service.SetRegras(idUsuario, idTelas, idRegras);

            return RedirectToAction("Index", "Usuario");
        }
    }
}