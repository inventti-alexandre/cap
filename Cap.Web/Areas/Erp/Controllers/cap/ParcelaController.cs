using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "parcela-r")]
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

        public PartialViewResult Parcelas(int idPedido, bool soAtivos = true)
        {
            var parcelas = service.Listar()
                .Where(x => x.IdPedido == idPedido
                        && (soAtivos == false || (soAtivos == true && x.Ativo == true)))
                        .OrderBy(x => x.Vencto)
                        .ToList();

            ViewBag.ValorTotal = parcelas.Sum(x => x.Valor).ToString("c2");
            ViewBag.IdPedido = idPedido;
            return PartialView(parcelas);
        }

        // GET: Erp/Parcela/Details/5
        public ActionResult Details(int id)
        {
            var parcela = service.Find(id);

            return PartialView(parcela);
        }

        // GET: Erp/Parcela/Create/5
        [AreaAuthorizeAttribute("Erp", Roles = "parcela-c")]
        public PartialViewResult Create(int idPedido)
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

        // GET: Erp/Parcela/Create
        [HttpPost]
        public ActionResult Create(ParcelaAdicionaModel model, int idPedido)
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
                            Observ = (model.Observ == null ? string.Empty : model.Observ),
                            Valor = model.Valor
                        };

                        switch (model.Periodicidade)
                        {
                            case Periodicidade.Mensal:
                                parcela.Vencto = model.Vencto.AddMonths(i * (int)model.Periodicidade);
                                break;
                            case Periodicidade.Semanal:
                                parcela.Vencto = model.Vencto.AddDays(i * (int)model.Periodicidade);
                                break;
                            case Periodicidade.Quinzenal:
                                parcela.Vencto = model.Vencto.AddDays(i * (int)model.Periodicidade);
                                break;
                            case Periodicidade.Bimestral:
                                parcela.Vencto = model.Vencto.AddMonths(i * (int)model.Periodicidade);
                                break;
                            case Periodicidade.Trimestral:
                                parcela.Vencto = model.Vencto.AddMonths(i * (int)model.Periodicidade);
                                break;
                            case Periodicidade.Semestral:
                                parcela.Vencto = model.Vencto.AddMonths(i * (int)model.Periodicidade);
                                break;
                            case Periodicidade.Anual:
                                parcela.Vencto = model.Vencto.AddMonths(i * (int)model.Periodicidade);
                                break;
                            case Periodicidade.Nenhuma:
                                parcela.Vencto = model.Vencto;
                                break;
                            default:
                                break;
                        }

                        // grava nova parcela
                        service.Gravar(parcela);
                    }

                    ViewBag.IdPedido = idPedido;
                    return Json(new { success = true });
                }

                ViewBag.IdPedido = idPedido;
                return PartialView(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.IdPedido = idPedido;
                return PartialView(model);
            }
        }

        // GET: Erp/Parcela/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "parcela-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parcela = service.Find((int)id);
            parcela.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            if (parcela == null)
            {
                return HttpNotFound();
            }

            // TODO: parcela paga tem que ter view diferenciada

            return PartialView(parcela);
        }

        // POST: Erp/Parcela/Edit/5
        [HttpPost]
        public ActionResult Edit(Parcela parcela)
        {
            try
            {
                parcela.AlteradoEm = DateTime.Now;
                parcela.NN = (parcela.NN == null ? "" : parcela.NN);
                TryUpdateModel(parcela);

                if (ModelState.IsValid)
                {
                    service.Gravar(parcela);
                    return Json(new { success = true });
                }

                // TODO: parcela paga tem que ter view diferenciada
                return PartialView(parcela);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(parcela);
            }
        }

        // GET: Erp/Parcela/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "parcela-d")]
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
    }
}
