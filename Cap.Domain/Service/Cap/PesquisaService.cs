using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Service.Cap
{
    public class PesquisaService
    {
        private EFDbContext ctx;

        public PesquisaService()
        {
            ctx = new EFDbContext();
        }

        public List<Parcela> Pesquisar(PesquisaModel pesquisa)
        {
            if (pesquisa.NF == null
                && pesquisa.Observ == null
                && pesquisa.NN == null
                && pesquisa.IdPedido == 0
                && pesquisa.IdFornecedor == 0
                && pesquisa.IdPgto == 0
                && pesquisa.IdFPgto == 0
                && pesquisa.Inicial == null
                && pesquisa.Final == null
                && pesquisa.Valor == 0
                && pesquisa.IdBanco == 0
                && pesquisa.IdConta == 0
                && pesquisa.Cheque == 0)
            {
                throw new ArgumentException("Nenhum parâmetro para pesquisa foi selecionado");
            }

            pesquisa.NF = (pesquisa.NF == null ? string.Empty : pesquisa.NF.ToUpper().Trim());
            pesquisa.Observ = (pesquisa.Observ ==  null ? string.Empty : pesquisa.Observ.ToUpper().Trim());
            pesquisa.NN = (pesquisa.NN == null ? string.Empty : pesquisa.NN);
            DateTime inicial;
            if (!DateTime.TryParse(pesquisa.Inicial, out inicial))
            {
                inicial = DateTime.MinValue;
            }
            DateTime final;
            if (!DateTime.TryParse(pesquisa.Final, out final))
            {
                final = DateTime.MinValue;
            }

            var parcelas = (from par in ctx.Parcela
                            join ped in ctx.Pedido on par.IdPedido equals ped.Id
                            join c in ctx.Conta on par.ContaId equals c.Id
                            where
                            (ped.Ativo == true && par.Ativo == true &&
                            (pesquisa.IdPedido == 0 || ped.Id == pesquisa.IdPedido) &&
                            (pesquisa.IdFornecedor == 0 || ped.IdFornecedor == pesquisa.IdFornecedor) &&
                            (pesquisa.NF == "" || ped.NF == pesquisa.NF) &&
                            (pesquisa.IdPgto == 0 || par.IdPgto == pesquisa.IdPgto) &&
                            (pesquisa.IdFPgto == 0 || par.IdFpgto == pesquisa.IdFPgto) &&
                            (pesquisa.Observ == "" || par.Observ.Contains(pesquisa.Observ)) &&
                            (pesquisa.NN == "" || par.NN == pesquisa.NN) &&
                            (pesquisa.Cheque == 0 || par.Cheque == pesquisa.Cheque) &&
                            (pesquisa.IdConta == 0 || par.ContaId == pesquisa.IdConta) &&
                            (pesquisa.IdBanco == 0 || c.Id == pesquisa.IdBanco)
                            ) select par);

            if (pesquisa.PesquisarPorDataPagamento == false)
            {
                if (inicial != DateTime.MinValue)
                {
                    parcelas = parcelas.Where(x => x.Vencto >= inicial);
                }
                if (final != DateTime.MinValue)
                {
                    parcelas = parcelas.Where(x => x.Vencto <= final);
                }
            }
            else
            {
                if (inicial != DateTime.MinValue)
                {
                    parcelas = parcelas.Where(x => x.BaixadoEm >= inicial);
                }
                if (final != DateTime.MinValue)
                {
                    parcelas = parcelas.Where(x => x.BaixadoEm <= final);
                }
            }

            if (pesquisa.Valor > 0)
            {
                if (pesquisa.MaiorQue == true)
                {
                    parcelas = parcelas.Where(x => x.Valor >= pesquisa.Valor);
                }
                else if (pesquisa.MenorQue == true)
                {
                    parcelas = parcelas.Where(x => x.Valor <= pesquisa.Valor);
                }
                else
                {
                    parcelas = parcelas.Where(x => x.Valor == pesquisa.Valor);
                }
            }

            return parcelas.ToList();
        }
    }
}
