﻿using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp",Roles ="admin")]
    public class FeriadoController : Controller
    {
        private IBaseService<Feriado> service;
        private ILogin login;

        public FeriadoController(IBaseService<Feriado> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Feriado
        public ActionResult Index(int? ano)
        {
            if (ano == null)
            {
                ano = DateTime.Today.Year;
            }

            ViewBag.ano = GetAnos((int)ano);
            ViewBag.AnoSelecionado = (int)ano;
            return View();
        }

        public PartialViewResult Feriados(int ano)
        {
            var feriados = service.Listar().Where(x => x.Data.Year == ano).OrderBy(x => x.Data).ToList();
            ViewBag.AnoSelecionado = ano;

            return PartialView(feriados);
        }

        // GET: Erp/Feriado/Details/5
        public ActionResult Details(int id)
        {
            var feriado = service.Find(id);

            if (feriado == null)
            {
                return HttpNotFound();
            }

            return View(feriado);
        }

        // GET: Erp/Feriado/Create
        public ActionResult Create(int ano)
        {
            ViewBag.Ano = ano;
            return View(new Feriado());
        }

        // POST: Erp/Feriado/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Data,Descricao")] Feriado feriado)
        {
            try
            {
                feriado.AlteradoEm = DateTime.Now;
                feriado.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(feriado);

                if (ModelState.IsValid)
                {
                    service.Gravar(feriado);
                    return RedirectToAction("Index", new { ano = feriado.Data.Year });
                }

                if (feriado.Data == DateTime.MinValue)
                {
                    ViewBag.Ano = DateTime.Today.Date.Year;
                }
                else
                {
                    ViewBag.Ano = feriado.Data.Year;
                }
                return View(feriado);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                if (feriado.Data == DateTime.MinValue)
                {
                    ViewBag.Ano = DateTime.Today.Date.Year;
                }
                else
                {
                    ViewBag.Ano = feriado.Data.Year;
                }
                return View(feriado);
            }
        }

        // GET: Erp/Feriado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var feriado = service.Find((int)id);

            if (feriado == null)
            {
                return HttpNotFound();
            }

            ViewBag.AnoSelecionado = feriado.Data.Year;
            return View(feriado);
        }

        // POST: Erp/Feriado/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Data,Descricao")] Feriado feriado)
        {
            try
            {
                feriado.AlteradoEm = DateTime.Now;
                feriado.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(feriado);

                if (ModelState.IsValid)
                {
                    service.Gravar(feriado);
                    return RedirectToAction("Index", new { ano = feriado.Data.Year });
                }

                if (feriado.Data == DateTime.MinValue)
                {
                    ViewBag.Ano = DateTime.Today.Date.Year;
                }
                else
                {
                    ViewBag.Ano = feriado.Data.Year;
                }
                return View(feriado);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                if (feriado.Data == DateTime.MinValue)
                {
                    ViewBag.Ano = DateTime.Today.Date.Year;
                }
                else
                {
                    ViewBag.Ano = feriado.Data.Year;
                }
                return View(feriado);
            }
        }

        // GET: Erp/Feriado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var feriado = service.Find((int)id);

            if (feriado == null)
            {
                return HttpNotFound();
            }

            return View(feriado);
        }

        // POST: Erp/Feriado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var feriado = service.Excluir(id);
                return RedirectToAction("Index", new { ano = feriado.Data.Year } );
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var feriado = service.Find(id);
                if (feriado == null)
                {
                    return HttpNotFound();
                }
                return View(feriado);
            }
        }

        private SelectList GetAnos(int ano = 0)
        {
            if (ano == 0)
            {
                ano = DateTime.Today.Date.Year;
            }

            List<int> anos = new List<int>();

            for (int i = DateTime.Today.Date.Year + 1; i > DateTime.Today.Date.Year - 10; i--)
            {
                anos.Add(i);
            }

            return new SelectList(anos, ano.ToString());
        }
    }
}