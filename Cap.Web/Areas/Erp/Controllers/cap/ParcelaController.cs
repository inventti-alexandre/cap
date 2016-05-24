using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class ParcelaController : Controller
    {
        IBaseService<Parcela> service;
        IBaseService<Moeda> moedaService;
        ILogin login;

        public ParcelaController(IBaseService<Parcela> service, IBaseService<Moeda> moedaService, ILogin login)
        {
            this.service = service;
            this.moedaService = moedaService;
            this.login = login;
        }

        // GET: Erp/Parcela
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Parcelas(int idPedido, bool soAtivos = true)
        {
            var parcelas = service.Listar()
                .Where(x => x.IdPedido == idPedido
                        && (soAtivos == false || (soAtivos == true && x.Ativo == true)))
                        .OrderBy(x => x.Vencto)
                        .ToList();

            ViewBag.IdPedido = idPedido;
            return PartialView(parcelas);
        }

        // GET: Erp/Parcela/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Erp/Parcela/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Erp/Parcela/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Erp/Parcela/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Erp/Parcela/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Erp/Parcela/Delete/5
        public ActionResult Delete(int id)
        {
            var parcela = service.Find(id);

            if (parcela == null)
            {
                return HttpNotFound();
            }

            return PartialView(parcela);
        }

        // POST: Erp/Parcela/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var parcela = service.Excluir(id);
                return RedirectToAction("Edit", "Pedido", new { id = parcela.IdPedido });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var parcela = service.Find(id);
                if (parcela == null)
                {
                    return HttpNotFound();
                }
                return PartialView(parcela);
            }
        }

        public PartialViewResult ParcelaAdiciona(int idPedido)
        {
            ViewBag.IdPedido = idPedido;
            return PartialView(new ParcelaAdicionaModel
            {
                Parcelas = 1,
                IdMoeda = moedaService.Listar().Where(x => x.Padrao == true).FirstOrDefault().Id,
                Periodicidade = Periodicidade.Mensal,
                Vencto = DateTime.Today.Date.AddMonths(1)
            });
        }

        [HttpPost]
        public ActionResult ParcelaAdiciona(ParcelaAdicionaModel model, int idPedido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

                    for (int i = 0; i < model.Parcelas; i++)
                    {
                        // TODO: deposito....
                        var parcela = new Parcela
                        {
                            AlteradoEm = DateTime.Now,
                            AlteradoPor = usuario.Id,
                            Ativo = true,
                            CriadoEm = DateTime.Now,
                            CriadoPor = usuario.Id,
                            IdEmpresa = usuario.IdEmpresa,
                            IdPgto = model.IdPgto,
                            IdPedido = idPedido,
                            IdMoeda = model.IdMoeda,
                            Observ = (model.Observ == null ? string.Empty :  model.Observ),
                            Valor = model.Valor,
                            Vencto = model.Vencto                             
                        };
                        // grava nova parcela
                        service.Gravar(parcela);
                    }

                    return RedirectToAction("Edit", "Pedido", new { id = idPedido });
                }

                return PartialView(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(model);
            }
        }
    }
}
