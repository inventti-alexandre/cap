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
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class UnidadeController : Controller
    {
        IBaseService<Unidade> service;
        ILogin login;

        public UnidadeController(IBaseService<Unidade> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Unidade
        public ActionResult Index()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var unidades = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(unidades);
        }

        // GET: Erp/Unidade/Details/5
        public ActionResult Details(int id)
        {
            var unidade = service.Find(id);

            if (unidade == null)
            {
                return HttpNotFound();
            }

            return View(unidade);
        }

        // GET: Erp/Unidade/Create
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(new Unidade { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa });
        }

        // POST: Erp/Unidade/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Descricao,IdEmpresa,AlteradoPor")] Unidade unidade)
        {
            try
            {
                unidade.AlteradoEm = DateTime.Now;
                TryUpdateModel(unidade);

                if (ModelState.IsValid)
                {
                    service.Gravar(unidade);
                    return RedirectToAction("Index");
                }

                return View(unidade);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(unidade);
            }
        }

        // GET: Erp/Unidade/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unidade = service.Find((int)id);

            if (unidade == null)
            {
                return HttpNotFound();
            }

            unidade.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(unidade);
        }

        // POST: Erp/Unidade/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Descricao,IdEmpresa,AlteradoPor")] Unidade unidade)
        {
            try
            {
                unidade.AlteradoEm = DateTime.Now;
                TryUpdateModel(unidade);

                if (ModelState.IsValid)
                {
                    service.Gravar(unidade);
                    return RedirectToAction("Index");
                }

                return View(unidade);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(unidade);
            }
        }

        // GET: Erp/Unidade/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unidade = service.Find((int)id);

            if (unidade == null)
            {
                return HttpNotFound();
            }

            return View(unidade);
        }

        // POST: Erp/Unidade/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var unidade = service.Find(id);
                if (unidade == null)
                {
                    return HttpNotFound();
                }
                return View(unidade);
            }
        }
    }
}
