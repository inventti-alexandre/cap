using Cap.Domain.Abstract.Cap;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Models.Cap;
using Cap.Domain.Abstract;

namespace Cap.Domain.Service.Cap
{
    public class CaixaService: ICaixa
    {
        private EFDbContext ctx;
        private IBaseService<InfoCaixa> serviceInfoCaixa;

        public CaixaService()
        {
            this.ctx = new EFDbContext();
            this.serviceInfoCaixa = new InfoCaixaService();
        }

        public List<Parcela> BaixarParcelas(List<int> idParcelas, int idUsuario, int idConta, int idCheque, DateTime caixaDia)
        {
            throw new NotImplementedException();
        }

        public List<Parcela> EstornarCheque(int idConta, int idCheque, int idUsuario)
        {
            throw new NotImplementedException();
        }

        public List<Parcela> GetParcelas(int idEmpresa, DateTime inicial, DateTime final, int idDepartamento, int idFornecedor, int idPgto)
        {
            var parcelas = (from par in ctx.Parcela
                            join ped in ctx.Pedido on par.IdPedido equals ped.Id
                            join f in ctx.Fornecedor on ped.IdFornecedor equals f.Id
                            join p in ctx.Pgto on par.IdPgto equals p.Id
                            where
                            ped.Ativo == true
                            && par.IdEmpresa == idEmpresa
                            && par.IdFpgto == null
                            && (idDepartamento == 0 || ped.IdDepartamento == idDepartamento)
                            && (idFornecedor == 0 || ped.IdFornecedor == idFornecedor)
                            && (idPgto == 0 || par.IdPgto == idPgto)
                            && ((par.Vencto >= inicial && par.Vencto <= final) ||
                            (par.EmissaoPre >= inicial && par.EmissaoPre <= final))
                            && par.LibMaster == true
                            orderby p.Descricao, f.Fantasia
                            select par)
                            .ToList();

            return parcelas;                
        }

        public decimal GetTotalSelecionado(List<int> idParcelas)
        {
            if (idParcelas.Count == 0)
            {
                return 0;
            }

            return ctx.Parcela.Where(x => idParcelas.Contains(x.Id)).Sum(x => x.Valor);                
        }
    }
}
