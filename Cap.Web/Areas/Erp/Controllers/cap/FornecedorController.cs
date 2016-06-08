using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;
using Cap.Web.Areas.Erp.Models;
using System.Net;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "fornecedor-r")]
    public class FornecedorController : Controller
    {
        private IBaseService<Fornecedor> service;
        private IBaseService<Agenda> serviceAgenda;
        private ILogin login;

        public FornecedorController(IBaseService<Fornecedor> service, IBaseService<Agenda> serviceAgenda, ILogin login)
        {
            this.service = service;
            this.serviceAgenda = serviceAgenda;
            this.login = login;
        }

        // GET: Erp/Fornecedor
        public ActionResult Index(string pesquisa = "")
        {
            ViewBag.Pesquisa = pesquisa;
            return View();
        }

        public PartialViewResult Fornecedores(string pesquisa = "")
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            pesquisa = pesquisa.ToUpper().Trim();

            if (string.IsNullOrEmpty(pesquisa))
            {
                return PartialView(new List<Fornecedor>());
            }

            var fornecedores = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa && x.Fantasia.Contains(pesquisa))
                .OrderBy(x => x.Fantasia)
                .ToList();
                
            ViewBag.Pesquisa = pesquisa;
            return PartialView(fornecedores);
        }

        // GET: Erp/Fornecedor/Details/5
        public ActionResult Details(int id, string pesquisa = "")
        {
            var fornecedor = service.Find(id);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }

            ViewBag.Pesquisa = pesquisa;
            return View(fornecedor);
        }

        // GET: Erp/Fornecedor/Create
        [AreaAuthorizeAttribute("Erp", Roles = "fornecedor-c")]
        public ActionResult Create(string pesquisa = "")
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var model = new FornecedorModel() { IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id };

            ViewBag.Pesquisa = pesquisa;
            return View(model);
        }

        // POST: Erp/Fornecedor/Create
        [HttpPost]
        public ActionResult Create(FornecedorModel model)
        {
            try
            {
                ViewBag.Pesquisa = model.Fantasia;

                if (ModelState.IsValid)
                {
                    var agenda = new Agenda()
                    {
                        AlteradoEm = DateTime.Now,
                        AlteradoPor = model.AlteradoPor,
                        Ativo = true,
                        Bairro = model.Bairro,
                        Cidade = model.Cidade,
                        Cep = model.Cep,
                        Contato = model.Contato,
                        Endereco = model.Endereco,
                        IdEmpresa = model.IdEmpresa,
                        IdEstado = model.IdEstado,
                        Nome = model.Fantasia,
                        WebSite = model.WebSite
                    };
                    model.IdAgenda = serviceAgenda.Gravar(agenda);

                    var fornecedor = new Fornecedor()
                    {
                        AlteradoEm = DateTime.Now,
                        AlteradoPor = model.AlteradoPor,
                        Ativo = true,
                        CNPJ = model.CNPJ,
                        Concessionaria = model.Concessionaria,
                        Contato = model.Contato,
                        Fantasia = model.Fantasia,
                        IdAgenda = model.IdAgenda,
                        IdEmpresa = model.IdEmpresa,
                        IdPgto = model.IdPgto,
                        IE = model.IE,
                        Imposto = model.Imposto,
                        Observ = model.Observ,
                        Razao = model.Razao
                    };
                    service.Gravar(fornecedor);
                    return RedirectToAction("Edit", new { id = fornecedor.Id, pesquisa = model.Fantasia.ToUpper().Trim() });
                }

                return View(model);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
        }

        // GET: Erp/Fornecedor/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "fornecedor-u")]
        public ActionResult Edit(int? id, string pesquisa = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fornecedor = service.Find((int)id);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }

            ViewBag.Pesquisa = pesquisa;
            fornecedor.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(fornecedor);
        }

        // POST: Erp/Fornecedor/Edit/5
        [HttpPost]
        public ActionResult Edit(Fornecedor fornecedor, string pesquisa = "")
        {
            ViewBag.Pesquisa = pesquisa;
            try
            {
                fornecedor.AlteradoEm = DateTime.Now;

                if (ModelState.IsValid)
                {
                    service.Gravar(fornecedor);
                    return RedirectToAction("Index", new { pesquisa = pesquisa });
                }

                return View(fornecedor);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(pesquisa);
            }
        }

        // GET: Erp/Fornecedor/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "fornecedor-d")]
        public ActionResult Delete(int? id, string pesquisa = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fornecedor = service.Find((int)id);

            if (fornecedor != null)
            {
                return HttpNotFound();
            }

            return View(fornecedor);
        }

        // POST: Erp/Fornecedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, string pesquisa = "")
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var fornecedor = service.Find(id);
                if (fornecedor == null)
                {
                    return HttpNotFound();
                }
                return View(fornecedor);
            }
        }
    }
}
