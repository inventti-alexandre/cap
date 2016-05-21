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
            pesquisa.NF = (pesquisa.NF == null ? string.Empty : pesquisa.NF.ToUpper().Trim());
            pesquisa.Observ = (pesquisa.Observ ==  null ? string.Empty : pesquisa.Observ.ToUpper().Trim());
            pesquisa.NN = (pesquisa.NN == null ? string.Empty : pesquisa.NN);

            // TODO: pesquisar formas de pagamento
            // PesquisarPorDataPagamento 1/2
            // IdBanco
            // IdConta
            // Cheque

            var parcelas = (from par in ctx.Parcela
                    join ped in ctx.Pedido on par.IdPedido equals ped.Id
                    where
                    ((pesquisa.IdPedido == 0 || ped.Id == pesquisa.IdPedido) &&
                    (pesquisa.IdFornecedor == 0 || ped.IdFornecedor == pesquisa.IdFornecedor) &&
                    (pesquisa.NF == "" || ped.NF == pesquisa.NF) &&
                    (pesquisa.PesquisarPorDataPagamento == true || (pesquisa.PesquisarPorDataPagamento == false  && (pesquisa.Inicial == DateTime.MinValue || par.Vencto >= pesquisa.Inicial) &&
                    (pesquisa.Final == DateTime.MinValue || par.Vencto <= pesquisa.Final))) &&
                    (
                        (pesquisa.MaiorQue == false && pesquisa.MenorQue == false && (pesquisa.Valor == 0 || par.Valor == pesquisa.Valor)) ||
                        (pesquisa.MaiorQue == false && pesquisa.MenorQue == true && (pesquisa.Valor == 0 || par.Valor == pesquisa.Valor)) ||
                        (pesquisa.MaiorQue == true && pesquisa.MenorQue == false && (pesquisa.Valor == 0 || par.Valor == pesquisa.Valor)) ||
                        (pesquisa.MaiorQue == true && pesquisa.MenorQue == true && (pesquisa.Valor == 0 || par.Valor == pesquisa.Valor))
                    ) &&
                    (pesquisa.IdPgto == 0 || par.IdPgto == pesquisa.IdPgto) &&
                    (pesquisa.IdFPgto == 0 || par.IdFpgto == pesquisa.IdFPgto) &&
                    (pesquisa.Observ == "" || par.Observ.Contains(pesquisa.Observ)) &&
                    (pesquisa.NN == "" || par.NN == pesquisa.NN)
                    )
                    select par).ToList();

            return parcelas;
        }
    }
}
