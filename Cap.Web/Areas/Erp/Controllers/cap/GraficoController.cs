using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using System;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class GraficoController : Controller
    {
        private IGrafico service;
        private ISistemaConfig config;
        private ILogin login;

        public GraficoController(IGrafico service, ILogin login, ISistemaConfig config)
        {
            this.service = service;
            this.login = login;
            this.config = config;
        }

        // GET: Erp/Grafico
        public ActionResult Index()
        {
            int idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            ViewBag.Dias = config.GetConfig(idEmpresa).GraficoDias;

            return View();
        }

        public Chart GetGrafico(int dias, int idDepartamento, int idPgto)
        {
            try
            {
                int idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
                DateTime inicial = DateTime.Today.Date;
                DateTime final = inicial.AddDays(dias);
                var grafico = service.GetGrafico(inicial, final, idEmpresa, idDepartamento, idPgto);

                StringBuilder t = new StringBuilder();
                t.Append("<Chart>")
                    .Append("<ChartAreas>")
                        .Append("<ChartArea Name='Default' _Template_='All'>")
                            .Append("<AxisY>")
                                .Append("<LabelStyle Font='Verdana, 12px' />")
                            .Append("</AxisY>")
                            .Append("<AxisX LineColor='64, 64, 64, 64' Interval='1'>")
                                .Append("<LabelStyle Font='Verdana, 12px' />")
                            .Append("</AxisX>")
                        .Append("</ChartArea>")
                    .Append("</ChartAreas>")
                .Append("</Chart>");

                Chart myChart = new Chart(800, 600, theme: t.ToString())
                    .AddSeries(
                    legend: "#VALX",
                    name: "Vencimentos",
                    xValue: grafico.Select(x => x.Dia).ToArray(),
                    yValues: grafico.Select(x => x.Valor).ToArray())
                    .SetXAxis("Dias")
                    .SetYAxis("Valores agendados")
                .Write();

                return myChart;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}