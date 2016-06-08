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
    [AreaAuthorizeAttribute("Erp", Roles = "socio-r")]
    public class SocioController : Controller
    {
        IBaseService<Socio> service;
        IBaseService<Empresa> empresa;
        ILogin login;

        public SocioController(IBaseService<Socio> service, IBaseService<Empresa> empresa, ILogin login)
        {
            this.service = service;
            this.empresa = empresa;
            this.login = login;
        }

        // GET: Erp/Socio
        public ActionResult Index(int idEmpresa)
        {
            var socios = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Nome).ToList();

            ViewBag.Empresa = empresa.Find(idEmpresa);
            return View(socios);
        }

        // GET: Erp/Socio/Details/5
        public ActionResult Details(int id)
        {
            var socio = service.Find(id);

            if (socio == null)
            {
                return HttpNotFound();
            }

            return View(socio);
        }

        // GET: Erp/Socio/Create
        [AreaAuthorizeAttribute("Erp", Roles = "socio-c")]
        public ActionResult Create(int idEmpresa)
        {
            return View(new Socio { IdEmpresa = idEmpresa, Nacionalidade = "BRASILEIRA", AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name) });
        }

        // POST: Erp/Socio/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="IdEmpresa,Nome,Endereco,Bairro,Cidade,IdEstado,Cep,Telefone,Email,Cpf,Nascimento,Conjuge,Profissao,Nacionalidade,IdEstadoCivil,AlteradoPor")] Socio socio)
        {
            try
            {
                socio.AlteradoEm = DateTime.Now;
                TryUpdateModel(socio);

                if (ModelState.IsValid)
                {
                    service.Gravar(socio);
                    return RedirectToAction("Index", new { idEmpresa = socio.IdEmpresa });
                }

                return View(socio);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(socio);
            }
        }

        // GET: Erp/Socio/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "socio-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var socio = service.Find((int)id);

            if (socio == null)
            {
                return HttpNotFound();
            }

            socio.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(socio);
        }

        // POST: Erp/Socio/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IdEmpresa,Nome,Endereco,Bairro,Cidade,IdEstado,Cep,Telefone,Email,Cpf,Nascimento,Conjuge,Profissao,Nacionalidade,IdEstadoCivil,Ativo,AlteradoPor")] Socio socio)
        {
            try
            {
                socio.AlteradoEm = DateTime.Now;
                TryUpdateModel(socio);

                if (ModelState.IsValid)
                {
                    service.Gravar(socio);
                    return RedirectToAction("Index", new { idEmpresa = socio.IdEmpresa });
                }

                return View(socio);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(socio);
            }
        }

        // GET: Erp/Socio/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "socio-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var socio = service.Find((int)id);

            if (socio == null)
            {
                return HttpNotFound();
            }

            return View(socio);
        }

        // POST: Erp/Socio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int idEmpresa)
        {
            try
            {
                var socio = service.Excluir(id);
                return RedirectToAction("Index", new { idEmpresa = socio.IdEmpresa });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var socio = service.Find(id);
                if (socio == null)
                {
                    return HttpNotFound();
                }
                return View(socio);
            }
        }
    }
}
