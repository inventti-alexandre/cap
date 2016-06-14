using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Gen;
using Cap.Domain.Models.Gen;
using Cap.Web.Areas.Erp.Models;
using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp", Roles = "indvariacao-r")]
    public class IndVariacaoController : Controller
    {
        private IBaseService<IndVariacao> service;
        private IBaseService<Indice> serviceIndice;
        private IIndVariacaoCalculo calc;
        private ILogin login;

        public IndVariacaoController(IBaseService<IndVariacao> service, IBaseService<Indice> serviceIndice, ILogin login, IIndVariacaoCalculo calc)
        {
            this.service = service;
            this.serviceIndice = serviceIndice;
            this.login = login;
            this.calc = calc;
        }

        // GET: Erp/IndVariacao
        public ActionResult Index(int id)
        {
            ViewBag.IdIndice = id;
            var menorAno = service.Listar().Min(x => x.DataVariacao.Year);
            var maiorAno = service.Listar().Max(x => x.DataVariacao.Year);

            int[] anos = new int[(maiorAno - menorAno) + 1];
            for (int i = 0; i < ((maiorAno - menorAno) + 1); i++)
            {
                anos[i] = maiorAno - i;
            }

            ViewBag.Ano = new SelectList(anos, DateTime.Today.Date.Year);

            return View(serviceIndice.Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .AsEnumerable());
        }

        // GET: Erp/IndVariacao/Details/5
        public ActionResult Details(int id)
        {
            var variacao = service.Find(id);

            if (variacao == null)
            {
                return HttpNotFound();
            }

            return PartialView(variacao);
        }

        public PartialViewResult Variacoes(int idIndice, DateTime? inicial, int? ano)
        {
            IEnumerable<IndVariacao> variacoes;

            if (inicial != null)
            {
                variacoes = service.Listar()
                    .Where(x => x.IdIndice == idIndice
                    && x.DataVariacao >= (DateTime)inicial)
                    .OrderBy(x => x.DataVariacao)
                    .AsEnumerable();
            } else {
                if (ano == null)
                {
                    ano = DateTime.Today.Year;
                }
                variacoes = service.Listar()
                    .Where(x => x.IdIndice == idIndice
                    && x.DataVariacao.Year == (int)ano)
                    .OrderBy(x => x.DataVariacao)
                    .AsEnumerable();
            }

            ViewBag.IdIndice = idIndice;
            ViewBag.Indice = serviceIndice.Find(idIndice).Descricao;
            return PartialView(variacoes);
        }

        public ActionResult VariacoesIndices(DateTime? inicial, DateTime? final)
        {
            if (inicial == null)
            {
                DateTime dataTemp = DateTime.Today.Date.AddYears(-1);
                inicial = new DateTime(dataTemp.Year, dataTemp.Month, 1);
            }

            if (final == null)
            {
                final = DateTime.Today.Date;
            }
  
            var indices = serviceIndice.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao);
            var lista = new List<List<IndVariacao>>();
            foreach (var item in indices)
            {
                lista.Add(service.Listar()
                    .Where(x => x.IdIndice == item.Id
                    && x.DataVariacao >= inicial && x.DataVariacao <= final)
                    .OrderBy(x => x.DataVariacao)
                    .ToList());
            }

            return View(lista);
        }

        // GET: Erp/IndVariacao/Create
        [AreaAuthorizeAttribute("Erp", Roles = "indvariacao-c")]
        public ActionResult Create(int idIndice)
        {
            var item = new IndVariacao
            {
                AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name),
                DataVariacao = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1),
                IdIndice = idIndice,
                Variacao = 0
            };

            ViewBag.IdIndice = idIndice;
            ViewBag.Indice = serviceIndice.Find(idIndice).Descricao;
            return PartialView(item);
        }

        // POST: Erp/IndVariacao/Create
        [HttpPost]
        public ActionResult Create(IndVariacao item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/IndVariacao/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "indvariacao-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IndVariacao item = service.Find((int)id);

            if (item == null)
            {
                return HttpNotFound();
            }

            ViewBag.Indice = serviceIndice.Find(item.IdIndice).Descricao;
            return PartialView(item);
        }

        // POST: Erp/IndVariacao/Edit/5
        [HttpPost]
        public ActionResult Edit(IndVariacao item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView();
            }
        }

        // GET: Erp/IndVariacao/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "indvariacao-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IndVariacao item = service.Find((int)id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // POST: Erp/IndVariacao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return Json(new { success = true });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var item = service.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                return PartialView(item);
            }
        }

        // GET: Erp/IndVariacao/Acumulado
        public PartialViewResult Acumulado(int? idIndice)
        {
            if (idIndice == null)
            {
                idIndice = 0;
            }

            var menorAno = service.Listar().Min(x => x.DataVariacao.Year);
            var maiorAno = service.Listar().Max(x => x.DataVariacao.Year);
            List<int> anos = new List<int>();
            for (int i = 0; i < ((maiorAno - menorAno) + 1); i++)
            {
                anos.Add(menorAno + i);
            }

            ViewBag.MaiorAno = maiorAno;
            ViewBag.MenorAno = maiorAno - 1;
            ViewBag.MaiorMes = DateTime.Today.Date.AddMonths(-1).Month;
            ViewBag.MenorMes = DateTime.Today.Date.AddYears(-1).Month;
            ViewBag.AnosInicial = new SelectList(anos, menorAno);
            ViewBag.AnosFinal = new SelectList(anos, maiorAno);

            return PartialView();
        }

        public ActionResult CalculoAcumulado(int idIndice, int anoInicial, int anoFinal, int mesInicial, int mesFinal)
        {
            DateTime inicial = new DateTime(anoInicial, mesInicial, 1);
            DateTime final = new DateTime(anoFinal, mesFinal, 1);
            ViewBag.Indice = serviceIndice.Find(idIndice).Descricao;
            ViewBag.Acumulado = string.Format("{0:N4}%", calc.CalcularVariacao(idIndice, inicial, final));
            ViewBag.Inicial = string.Format("{0:MM/yyyy}", inicial);
            ViewBag.Final = string.Format("{0:MM/yyyy}", final);
            return PartialView();
        }
    }
}
