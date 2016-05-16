using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;
using System.Net;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Abstract.Admin;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class MaterialController : Controller
    {
        IBaseService<Material> service;
        ILogin login;

        public MaterialController(IBaseService<Material> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Material
        public ActionResult Index(string pesquisa = "")
        {
            ViewBag.Pesquisa = pesquisa;
            return View();
        }

        public PartialViewResult Materiais(string pesquisa = "")
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            pesquisa = pesquisa.ToUpper().Trim();

            if (string.IsNullOrEmpty(pesquisa))
            {
                return PartialView(new List<Material>());
            }

            var materiais = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa && x.Descricao.Contains(pesquisa))
                .OrderBy(x => x.Descricao)
                .ToList();

            ViewBag.Pesquisa = pesquisa;
            return PartialView(materiais);
        }

        // GET: Erp/Material/Details/5
        public ActionResult Details(int id, string pesquisa = "")
        {
            var material = service.Find(id);

            if (material == null)
            {
                return HttpNotFound();
            }

            ViewBag.Pesquisa = pesquisa;
            return View(material);
        }

        // GET: Erp/Material/Create
        public ActionResult Create(string pesquisa = "")
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            ViewBag.Pesquisa = pesquisa;
            return View(new Material { IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id });
        }

        // POST: Erp/Material/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdEmpresa,Descricao,IdUnidade,IdMatGrupo,PrazoMinimoEntrega,EstoqueAtual,CompraAutomatica,QtdeMinimaPedido,AlteradoPor")] Material material)
        {
            try
            {
                material.AlteradoEm = DateTime.Now;
                TryUpdateModel(material);

                if (ModelState.IsValid)
                {
                    service.Gravar(material);
                    return RedirectToAction("Index", new { pesquisa = material.Descricao.ToUpper().Trim() });
                }

                ViewBag.Pesquisa = material.Descricao.ToUpper().Trim();
                return View(material);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Pesquisa = material.Descricao.ToUpper().Trim();
                return View(material);
            }
        }

        // GET: Erp/Material/Edit/5
        public ActionResult Edit(int? id, string pesquisa = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var material = service.Find((int)id);

            material.AlteradoEm = DateTime.Now;
            ViewBag.Pesquisa = pesquisa;
            return View(material);
        }

        // POST: Erp/Material/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IdEmpresa,Descricao,IdUnidade,IdMatGrupo,PrazoMinimoEntrega,EstoqueAtual,CompraAutomatica,QtdeMinimaPedido,AlteradoPor")] Material material)
        {
            try
            {
                material.AlteradoEm = DateTime.Now;
                TryUpdateModel(material);

                if (ModelState.IsValid)
                {
                    service.Gravar(material);
                    return RedirectToAction("Index", new { pesquisa = material.Descricao.ToUpper().Trim()});
                }

                ViewBag.Pesquisa = material.Descricao.ToUpper().Trim();
                return View(material);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Pesquisa = material.Descricao.ToUpper().Trim();
                return View(material);
            }
        }

        // GET: Erp/Material/Delete/5
        public ActionResult Delete(int? id, string pesquisa = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var material = service.Find((int)id);

            if (material == null)
            {
                return HttpNotFound();
            }

            ViewBag.Pesquisa = pesquisa;
            return View(material);
        }

        // POST: Erp/Material/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, string pesquisa = "")
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index", new { pesquisa = pesquisa });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var material = service.Find(id);
                if (material == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Pesquisa = pesquisa;
                return View(pesquisa);
            }
        }
    }
}
