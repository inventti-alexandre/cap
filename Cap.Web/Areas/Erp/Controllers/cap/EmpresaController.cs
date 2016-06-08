using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "empresa-r")]
    public class EmpresaController : Controller
    {
        IBaseService<Empresa> service;

        public EmpresaController(IBaseService<Empresa> service)
        {
            this.service = service;
        }

        // GET: Erp/Empresa
        public ActionResult Index()
        {
            var empresas = service.Listar()
                .OrderBy(x => x.Fantasia)
                .ToList();

            return View(empresas);
        }

        // GET: Erp/Empresa/Details/5
        public ActionResult Details(int id)
        {
            var empresa = service.Find(id);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(empresa);
        }

        // GET: Erp/Empresa/Create
        [AreaAuthorizeAttribute("Erp", Roles = "empresa-c")]
        public ActionResult Create()
        {
            return View(new Empresa());
        }

        // POST: Erp/Empresa/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Fantasia,Razao,Cnpj,IE,Endereco,Bairro,Cidade,IdEstado,Cep,Email,Telefone,ECnpj,ECnpjVencto,IdRegimeTributario")]Empresa empresa)
        {
            try
            {
                empresa.AlteradoEm = DateTime.Now;
                TryUpdateModel(empresa);

                if (ModelState.IsValid)
                {
                    service.Gravar(empresa);
                    return RedirectToAction("Index");
                }

                return View(empresa);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(empresa);
            }
        }

        // GET: Erp/Empresa/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "empresa-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = service.Find((int)id);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(empresa);
        }

        // POST: Erp/Empresa/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Fantasia,Razao,Cnpj,IE,Endereco,Bairro,Cidade,IdEstado,Cep,Email,Telefone,ECnpj,ECnpjVencto,Ativo,IdRegimeTributario")]Empresa empresa)
        {
            try
            {
                empresa.AlteradoEm = DateTime.Now;
                TryUpdateModel(empresa);

                if (ModelState.IsValid)
                {
                    service.Gravar(empresa);
                    return RedirectToAction("Index");
                }

                return View(empresa);

            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(empresa);
            }
        }

        // GET: Erp/Empresa/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "empresa-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = service.Find((int)id);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(empresa);
        }

        // POST: Erp/Empresa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var empresa = service.Find(id);
                if (empresa == null)
                {
                    return HttpNotFound();
                }
                return View(empresa);
            }
        }
    }
}
