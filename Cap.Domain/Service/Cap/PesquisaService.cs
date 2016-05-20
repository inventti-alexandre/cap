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

            return (from par in ctx.Parcela
                    join ped in ctx.Pedido on par.IdPedido equals ped.Id
                    where
                    (pesquisa.IdPedido == 0 || ped.Id == pesquisa.IdPedido)
                    select par).ToList();
        }
    }
}
