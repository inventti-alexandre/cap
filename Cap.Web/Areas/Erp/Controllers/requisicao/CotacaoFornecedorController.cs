using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using System;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    public class CotacaoFornecedorController : Controller
    {
        private IResumoCotacao service;
        private IBaseService<CotCotadoCom> serviceCotadoCom;

        public CotacaoFornecedorController(IResumoCotacao service, IBaseService<CotCotadoCom> serviceCotadoCom)
        {
            this.service = service;
            this.serviceCotadoCom = serviceCotadoCom;
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

        // GET: Erp/CotacaoFornecedor/Detalhamento/5
        public ActionResult Detalhamento(int id)
        {
            try
            {
                var resumo = service.GetResumo(id);

                return PartialView("Detalhamento", resumo.ResumoDetalhado);
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        // GET: Erp/CotacaoFornecedor/Excluir/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id = (int)id;
            return PartialView();
        }

        // POST: Erp/CotacaoFornecedor/Excluir/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                serviceCotadoCom.Excluir(id);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }
    }
}