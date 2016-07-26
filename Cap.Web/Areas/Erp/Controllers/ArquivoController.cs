using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers
{
    public class ArquivoController : Controller
    {
        private IBaseService<ArquivoMorto> service;
        private ILogin login;

        public ArquivoController(IBaseService<ArquivoMorto> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Arquivo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Erp/Arquivo/Pesquisa
        public ActionResult Pesquisar(int caixa, string conteudo, string observ, int idDepartamento)
        {
            if (caixa != 0 && string.IsNullOrEmpty(conteudo) && string.IsNullOrEmpty(observ) && idDepartamento != 0)
            {
                return PartialView(new List<ArquivoMorto>());
            }

            if (!string.IsNullOrEmpty(conteudo))
            {
                conteudo = conteudo.ToUpper().Trim();
            }
            else
            {
                conteudo = string.Empty;
            }

            if (!string.IsNullOrEmpty(observ))
            {
                observ = conteudo.ToUpper().Trim();
            }
            else
            {
                observ = conteudo.ToUpper().Trim();
            }

            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var arquivos = service.Listar()
                .Where(x => x.EmpresaId == idEmpresa
                && (caixa == 0 || x.Caixa == caixa)
                && (conteudo == null || x.Conteudo.Contains(conteudo))
                && (observ == null || x.Observ.Contains(observ))
                && (idDepartamento == 0 || x.DepartamentoId == idDepartamento))
                .ToList();

            return PartialView(arquivos);
        }

        // GET: Erp/Arquivo/Details/5
        public ActionResult Details(int id)
        {
            var arquivo = service.Find(id);

            if (arquivo == null)
            {
                return Json(new { success = false, error = "Arquivo não disponível" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(arquivo);
        }

        // GET: Erp/Arquivo/Create
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var arquivo = new ArquivoMorto
            {
                AlteradoEm = DateTime.Now,
                Caixa = getProximaCaixa(usuario.IdEmpresa),
                Conteudo = string.Empty,
                EmpresaId = usuario.IdEmpresa,
                UsuarioId = usuario.Id
            };

            return PartialView(arquivo);
        }

        // POST: Erp/Arquivo/Create
        [HttpPost]
        public ActionResult Create(ArquivoMorto item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true });
                }
                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                return Json(new { error = e.Message });
            }
        }

        // GET: Erp/Arquivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = service.Find((int)id);

            if (item == null)
            {
                return Json(new { success = false, error = "Arquivo inexistente" });
            }

            return PartialView(item);
        }

        // POST: Erp/Arquivo/Edit/5
        [HttpPost]
        public ActionResult Edit(ArquivoMorto item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(e);
            }
        }

        // GET: Erp/Arquivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = service.Find((int)id);

            if (item == null)
            {
                return Json(new { success = false, error = "Arquivo inexistente" });
            }

            return PartialView(item);
        }

        // POST: Erp/Arquivo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return Json(new { success = true });
            }
            catch (ArgumentException e)
            {
                var item = service.Find(id);

                if (item == null)
                {
                    return Json(new { error = "Arquivo inexistente" });
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }


        private int getProximaCaixa(int idEmpresa)
        {
            var arquivo = service.Listar()
                .Where(x => x.EmpresaId == idEmpresa)
                .OrderByDescending(x => x.Caixa)
                .First();

            if (arquivo == null)
            {
                return 1;
            }

            return arquivo.Caixa + 1;
        }

    }
}
