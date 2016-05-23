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

            // TODO: pesquisar formas de pagamento
            // IdBanco
            // IdConta
            // Cheque

            var parcelas = (from par in ctx.Parcela
                            join ped in ctx.Pedido on par.IdPedido equals ped.Id
                            where
                            ((pesquisa.IdPedido == 0 || ped.Id == pesquisa.IdPedido) &&
                            (pesquisa.IdFornecedor == 0 || ped.IdFornecedor == pesquisa.IdFornecedor) &&
                            (pesquisa.NF == "" || ped.NF == pesquisa.NF) &&
                            (pesquisa.IdPgto == 0 || par.IdPgto == pesquisa.IdPgto) &&
                            (pesquisa.IdFPgto == 0 || par.IdFpgto == pesquisa.IdFPgto) &&
                            (pesquisa.Observ == "" || par.Observ.Contains(pesquisa.Observ)) &&
                            (pesquisa.NN == "" || par.NN == pesquisa.NN)
                            )
                            select par);

            if (pesquisa.PesquisarPorDataPagamento == false)
            {
                if (pesquisa.Inicial != null)
                {
                    parcelas = parcelas.Where(x => x.Vencto >= pesquisa.Inicial);
                }
                if (pesquisa.Final != null)
                {
                    parcelas = parcelas.Where(x => x.Vencto <= pesquisa.Final);
                }
            }
            else
            {
                // TODO: pesquisa por data de pagamento
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
