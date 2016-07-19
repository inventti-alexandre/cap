using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    public class CotacaoCompraController : Controller
    {
        private ICotacaoService serviceCotacao;
        private IBaseService<ReqRequisicao> serviceRequisicao;

        public CotacaoCompraController(ICotacaoService serviceCotacao, IBaseService<ReqRequisicao> serviceRequisicao)
        {
            this.serviceCotacao = serviceCotacao;
            this.serviceRequisicao = serviceRequisicao;
        }

        // GET: Erp/CotacaoCompra
        public ActionResult Index(int id)
        {
            var requisicao = serviceRequisicao.Find(id);

            if (requisicao == null)
            {
                return HttpNotFound();
            }

            return View(requisicao);
        }
    }
}