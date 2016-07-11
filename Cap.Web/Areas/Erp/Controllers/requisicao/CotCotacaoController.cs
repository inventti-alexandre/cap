using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    public class CotCotacaoController : Controller
    {
        IBaseService<CotCotacao> service;

        public CotCotacaoController(IBaseService<CotCotacao> service)
        {
            this.service = service;
        }

        // GET: Erp/CotCotacao
        public ActionResult Index(int id)
        {
            var cotacaoItem = service.Find(id);

            if (cotacaoItem == null)
            {
                return HttpNotFound();
            }

            return PartialView(cotacaoItem);
        }


         // GET: Erp/CotCotacao/Edit/5
        public ActionResult Edit(int id)
        {
            var cotacaoItem = service.Find(id);

            if (cotacaoItem == null)
            {
                return HttpNotFound();
            }

            return PartialView(cotacaoItem);
        }

        // POST: Erp/CotCotacao/Edit/5
        [HttpPost]
        public ActionResult Edit(CotCotacao item)
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
    }
}
