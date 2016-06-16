using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class LiberacaoService : ILiberacao
    {
        EFDbContext ctx;
        EFRepository<Parcela> repository;

        public LiberacaoService()
        {
            ctx = new EFDbContext();
            repository = new EFRepository<Parcela>();
        }

        public void CancelarLiberacao(int idParcela, int idUsuario)
        {
            var parcela = repository.Find(idParcela);

            if (parcela != null)
            {
                if (parcela.IdFpgto != 0)
                {
                    throw new ArgumentException("Esta parcela já foi paga");
                }
            }

            parcela.Liberado = false;
            parcela.LiberadoEm = null;
            parcela.LiberadoPor = null;
            repository.Alterar(parcela);
        }

        public void LiberarParcelas(List<int> idParcelas, int idUsuario)
        {
            foreach (var item in idParcelas)
            {
                var parcela = repository.Find(item);

                if (parcela != null)
                {
                    if (parcela.IdFpgto != 0)
                    {
                        parcela.Liberado = true;
                        parcela.LiberadoEm = DateTime.Now;
                        parcela.LiberadoPor = idUsuario;
                        repository.Alterar(parcela);
                    }
                }
            }
        }

        public List<Parcela> ParcelasALiberar(int idUsuario, DateTime? final)
        {
            if (final == null)
            {
                final = DateTime.Today.Date.AddDays(7);
            }

            var parcelas = (from p in ctx.Parcela
                            join ped in ctx.Pedido on p.IdPedido equals ped.Id
                            where ped.CriadoPor == idUsuario
                            && p.Vencto > DateTime.Today.Date
                            && p.Vencto < final
                            && p.LibMaster == false
                            && p.Liberado == false
                            && p.IdFpgto == 0
                            select p)
                            .OrderBy(x => x.Vencto)
                            .ToList();

            return parcelas;                            
        }
     
    }
}
