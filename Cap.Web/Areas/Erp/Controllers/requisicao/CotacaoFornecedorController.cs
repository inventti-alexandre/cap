using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    public class CotacaoFornecedorController : Controller
    {
        private IResumoCotacao service;

        public CotacaoFornecedorController(IResumoCotacao service)
        {
            this.service = service;
        }

        // GET: Erp/Cotacao/Resumo/5
        public ActionResult Resumo(int id)
        {
            try
            {
                var resumo = service.GetResumo(id);

                return View(resumo);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(new Resumo());
            }   
        }
    }
}