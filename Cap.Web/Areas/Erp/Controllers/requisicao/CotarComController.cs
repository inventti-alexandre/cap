using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Requisicao;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    [AreaAuthorizeAttribute("Erp", Roles = "cotarcom-r")]
    public class CotarComController : Controller
    {
        IBaseService<CotFornecedor> service;
        ILogin login;

        public CotarComController(IBaseService<CotFornecedor> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/CotarCom
        public ActionResult Index(int id = 0)
        {
            ViewBag.IdCotGrupo = id;
            return View();
        }

        public ActionResult Fornecedores(int id)
        {
            var fornecedores = service.Listar()
                .Where(x => x.CotGrupoId == id)
                .OrderBy(x => x.Fornecedor.Fantasia)
                .ToList();

            ViewBag.Id = id;
            return PartialView(fornecedores);
        }

        // GET: Erp/CotarCom/Details/5
        public ActionResult Details(int id)
        {
            var item = service.Find(id);

            return View(item);
        }

        // GET: Erp/CotarCom/Create
        [AreaAuthorizeAttribute("Erp", Roles = "cotarcom-c")]
        public ActionResult Create(int id)
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var item = new CotFornecedor { CotGrupoId = id, UsuarioId = idUsuario };

            return PartialView(item);
        }

        // POST: Erp/CotarCom/Create
        [HttpPost]
        public ActionResult Create(CotFornecedor item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true, id = item.CotGrupoId });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/CotarCom/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "cotarcom-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = service.Find((int)id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // POST: Erp/CotarCom/Edit/5
        [HttpPost]
        public ActionResult Edit(CotFornecedor item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true, id = item.CotGrupoId });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/CotarCom/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "cotarcom-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = service.Find((int)id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // POST: Erp/CotarCom/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = service.Excluir(id);
                return Json(new { success = true, id = item.UsuarioId });
            }
            catch (ArgumentException e)
            {
                var item = service.Find(id);

                if (item == null)
                {
                    return HttpNotFound();
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/CotarCom/CotarCom/5
        public ActionResult CotarCom(int idRequisicao)
        {
            ViewBag.IdRequisicao = idRequisicao;
            return PartialView();
        }

        // GET: Erp/CotarCom/GetSelecaoFornecedores
        public ActionResult GetSelecaoFornecedores(int idCotGrupo, int idRequisicao)
        {
            var fornecedores = service.Listar()
                .Where(x => x.CotGrupoId == idCotGrupo && x.Ativo == true)
                .OrderBy(x => x.Fornecedor.Fantasia)
                .ToList();

            ViewBag.IdCotGrupo = idCotGrupo;
            ViewBag.IdRequisicao = idRequisicao;
            return PartialView(fornecedores);
        }

        // POST: Erp/CotarCom/EnviarCotacao/
        [HttpPost]
        public ActionResult EnviarCotacao(int[] selecionados, int idRequisicao)
        {
            // parei aki e na View que chama este metodo (GetSelecaoFornecedores)
            return View();
        }

        // GET: Erp/CotarCom/EnviarPorEmail/
        public ActionResult EnviarPorEmail(int idRequisicao)
        {
            ViewBag.IdRequisicao = idRequisicao;
            return PartialView();
        }

        // POST: Erp/CotarCom/EnviarPorEmail/
        [HttpPost]
        public ActionResult EnviarPorEmail(int idRequisicao, string email)
        {
            // TODO: service to send email
            bool enviado = true;

            if (enviado == true)
            {
                return Json(new { success = true, message = $"Cotação enviada para { email }" });
            }

            return PartialView(idRequisicao);
        }
    }
}
