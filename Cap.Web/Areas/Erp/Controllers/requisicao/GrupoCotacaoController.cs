using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Requisicao;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    [AreaAuthorizeAttribute("Erp", Roles = "grupocotacao-r")]
    public class GrupoCotacaoController : Controller
    {
        IBaseService<CotGrupo> service;
        ILogin login;

        public GrupoCotacaoController(IBaseService<CotGrupo> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/GrupoCotacao
        public ActionResult Index()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var grupos = service.Listar()
                .Where(x => x.EmpresaId == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(grupos);
        }

        // GET: Erp/GrupoCotacao/Details/5
        public ActionResult Details(int id)
        {
            var item = service.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // GET: Erp/GrupoCotacao/Create
        [AreaAuthorizeAttribute("Erp", Roles = "grupocotacao-c")]
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var item = new CotGrupo { EmpresaId = usuario.IdEmpresa, UsuarioId = usuario.Id };

            return PartialView(item);
        }

        // POST: Erp/GrupoCotacao/Create
        [HttpPost]
        public ActionResult Create(CotGrupo item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

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
                return PartialView(item);
            }
        }

        // GET: Erp/GrupoCotacao/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupocotacao-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = service.Find((int)id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // POST: Erp/GrupoCotacao/Edit/5
        [HttpPost]
        public ActionResult Edit(CotGrupo item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

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
                return PartialView(item);
            }
        }

        // GET: Erp/GrupoCotacao/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupocotacao-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = service.Find((int)id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // POST: Erp/GrupoCotacao/Delete/5
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
                    return HttpNotFound();
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }
    }
}
