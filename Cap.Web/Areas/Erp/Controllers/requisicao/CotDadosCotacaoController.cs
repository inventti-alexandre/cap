using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    public class CotDadosCotacaoController : Controller
    {
        IBaseService<CotDadosCotacao> service;

        public CotDadosCotacaoController(IBaseService<CotDadosCotacao> service)
        {
            this.service = service;
        }

        // GET: Erp/CotDadosCotacao
        public ActionResult Index(int id)
        {
            var dados = service.Find(id);

            if (dados == null)
            {
                return HttpNotFound();
            }

            return PartialView(dados);
        }

        // GET: Erp/CotDadosCotacao/Edit/5
        public ActionResult Edit(int id)
        {
            var dados = service.Find(id);

            if (dados == null)
            {
                return HttpNotFound();
            }

            return PartialView(dados);
        }

        // POST: Erp/CotDadosCotacao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CotDadosCotacao dados)
        {
            try
            {
                dados.AlteradoEm = DateTime.Now;
                TryUpdateModel(dados);

                if (ModelState.IsValid)
                {
                    return Json(new { success = true });
                }

                return PartialView(dados);
            }
            catch (ArgumentException e)
            {
                return Json(new { error = e.Message });
            }
        }
    }
}
