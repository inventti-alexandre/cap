using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class GraficoService : IGrafico
    {
        private EFDbContext ctx;

        public GraficoService()
        {
            ctx = new EFDbContext();
        }

        public List<Grafico> GetGrafico(DateTime inicial, DateTime final, int idEmpresa, int idDepartamento = 0, int idPgto = 0)
        {
            // valida datas
            inicial = getDataInicial(inicial);
            final = getDataFinal(inicial, final);

            // lista de pagamentos para o periodo
            var parcelas = (from par in ctx.Parcela
                            join ped in ctx.Pedido on par.IdPedido equals ped.Id
                            where
                            ped.IdEmpresa == idEmpresa
                            && par.IdFpgto == null
                            && par.Vencto >= inicial
                            && par.Vencto <= final
                            && (idDepartamento == 0 || ped.IdDepartamento == idDepartamento)
                            && (idPgto == 0 || par.IdPgto == idPgto)
                            select new
                            {
                                Vencto = par.Vencto,
                                Valor = par.Valor
                            }
                            into p
                            group p by new { p.Vencto } into g
                            orderby g.Key.Vencto
                            select new
                            {
                                Vencto = g.Key.Vencto,
                                Valor = g.Sum(x => x.Valor)
                            });

            // dias no periodo -> lista grafico
            var grafico = new List<Grafico>();
            var dias = (final - inicial).TotalDays;
            for (int i = 0; i < dias; i++)
            {
                grafico.Add(new Grafico { Data = inicial.AddDays(i), Valor = 0, Inicial = inicial.AddDays(i), Final = inicial.AddDays(i) });
            }

            // valores existentes
            for (int i = 0; i < dias; i++)
            {
                DateTime data = inicial.AddDays(i);
                var item = parcelas.FirstOrDefault(x => x.Vencto == data);

                if (item != null)
                {
                    grafico[i].Valor = item.Valor;
                }
            }

            // lista de feriados
            var feriados = ctx.Feriado.Where(x => x.IdEmpresa == idEmpresa && x.Data >= inicial && x.Data <= final).Select(x => x.Data).ToList();

            // lista de pagamentos
            for (int i = 0; i < grafico.Count; i++)
            {
                // feriado
                if (feriados.Contains(grafico[i].Data) && i < grafico.Count - 1)
                {
                    grafico[i + 1].Valor = grafico[i + 1].Valor + grafico[i].Valor;
                    grafico[i].Valor = 0;
                    grafico[i].Feriado = true;
                    grafico[i].Dia = "fer";
                    grafico[i].Inicial = grafico[i].Data;
                    grafico[i].Final = grafico[i + 1].Data;
                }

                // sabado
                if (grafico[i].Data.DayOfWeek == DayOfWeek.Saturday && i < (grafico.Count - 2))
                {
                    grafico[i + 2].Valor = grafico[i + 2].Valor + grafico[i].Valor;
                    grafico[i].Valor = 0;
                    grafico[i].Dia = "sáb";
                    grafico[i].Final = grafico[i + 2].Final;
                }

                // domingo
                if (grafico[i].Data.DayOfWeek == DayOfWeek.Sunday && i < grafico.Count - 1)
                {
                    grafico[i + 1].Valor = grafico[i + 1].Valor + grafico[i].Valor;
                    grafico[i].Valor = 0;
                    grafico[i].Dia = "dom";
                    grafico[i].Final = grafico[i + 1].Data;
                }

                // feriado
                if (feriados.Contains(grafico[i].Data) && i < grafico.Count - 1)
                {
                    grafico[i + 1].Valor = grafico[i + 1].Valor + grafico[i].Valor;
                    grafico[i].Valor = 0;
                    grafico[i].Feriado = true;
                    grafico[i].Dia = "fer";
                    grafico[i].Final = grafico[i + 1].Data;
                }

                if (string.IsNullOrEmpty(grafico[i].Dia))
                {
                    grafico[i].Dia = grafico[i].Final.Day.ToString();
                }
            }

            return grafico;
        }

        private DateTime getDataFinal(DateTime inicial, DateTime final)
        {
            if (final < inicial)
            {
                final = inicial.AddMonths(30).AddDays(-1);
            }

            return final;
        }

        private DateTime getDataInicial(DateTime inicial)
        {
            if (inicial < DateTime.Today.Date)
            {
                inicial = DateTime.Today.Date;
            }

            return inicial;
        }
    }
}
