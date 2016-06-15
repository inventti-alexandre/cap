using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class DebitosController : Controller
    {
        IBaseService<Parcela> service;
        ILogin login;

        public DebitosController(IBaseService<Parcela> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Debitos
        public ActionResult Index()
       {
            var pesquisa = new PesquisaModel
            {
                IdDepartamento = 0,
                IdFornecedor = 0,
                IdPgto = 0,
                Inicial = DateTime.Today.Date,
                Final = DateTime.Today.Date.AddMonths(1),
                Observ = string.Empty
            };

            return View(pesquisa);
        }

        // GET: Erp/Debitos/Pesquisar
        public ActionResult Pesquisar(PesquisaModel pesquisa)
        {
            try
            {
                var parcelas = new PesquisaService().Pesquisar(pesquisa);

                return PartialView(parcelas);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}