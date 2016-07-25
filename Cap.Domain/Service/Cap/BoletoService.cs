using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class BoletoService : IBoleto
    {
        private IBaseService<Parcela> service;

        public BoletoService()
        {
            service = new ParcelaService();
        }

        public List<Parcela> GetBoletos(int idEmpresa, DateTime? inicial, DateTime? final)
        {
            inicial = getDataInicial(inicial);
            final = getDataFinal(final);

            return service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa
                && x.Vencto >= inicial
                && x.Vencto <= final
                && x.IdFpgto == null
                && x.Ativo == true
                && x.NN == "")
                .OrderBy(x => x.Vencto)
                .ToList();
        }

        public Parcela SetBoleto(int idParcela, string nn, int idUsuario, int idEmpresa)
        {
            try
            {
                var parcela = service.Find(idParcela);

                if (parcela == null)
                {
                    throw new ArgumentException("Parcela inexistente");
                }

                if (parcela.IdEmpresa != idEmpresa)
                {
                    throw new ArgumentException("Parcela inválida");
                }

                if (string.IsNullOrEmpty(nn) || nn.Length > 44)
                {
                    throw new ArgumentException("Nosso número inválido");
                }

                parcela.NN = nn;
                parcela.NNData = DateTime.Now;
                parcela.NNPor = idUsuario;
                service.Gravar(parcela);

                return parcela;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private DateTime? getDataFinal(DateTime? final)
        {
            if (final == null || final == DateTime.MinValue)
            {
                final = DateTime.Today.Date;
            }

            return final;
        }

        private static DateTime? getDataInicial(DateTime? inicial)
        {
            if (inicial == null || inicial == DateTime.MinValue)
            {
                if (DateTime.Today.Date.DayOfWeek == DayOfWeek.Monday)
                {
                    inicial = DateTime.Today.Date.AddDays(-2);
                }
                else if (DateTime.Today.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    inicial = DateTime.Today.Date.AddDays(-1);
                }
                else
                {
                    inicial = DateTime.Today.Date;
                }
            }

            return inicial;
        }
    }
}
