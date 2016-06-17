using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class LiberacaoPagamentoService : ILiberacaoPagamento
    {
        EFDbContext ctx;
        EFRepository<Parcela> repository;

        public LiberacaoPagamentoService()
        {
            ctx = new EFDbContext();
            repository = new EFRepository<Parcela>();
        }

        public void CancelarLiberacao(int idParcela, int idUsuario)
        {
            var parcela = repository.Find(idParcela);

            if (parcela != null)
            {
                if (parcela.IdFpgto != null)
                {
                    throw new ArgumentException("Esta parcela já foi paga");
                }
            }

            parcela.LibMaster = false;
            parcela.LibMasterEm = null;
            parcela.LibMasterPor = null;
            repository.Alterar(parcela);
        }

        public void LiberarParcelas(List<int> idParcelas, int idUsuario)
        {
            var usuario = ctx.Usuario.Find(idUsuario);

            if (usuario == null)
            {
                throw new ArgumentException("Usuário inválido");
            }

            foreach (var item in idParcelas)
            {
                var parcela = repository.Find(item);

                if (parcela != null)
                {
                    if (parcela.IdFpgto == null)
                    {
                        parcela.LibMaster = true;
                        parcela.LibMasterEm = DateTime.Now;
                        parcela.LibMasterPor = idUsuario;
                        repository.Alterar(parcela);
                    }
                }
            }
        }

        public List<Parcela> ParcelasACancelar(DateTime? final)
        {
            if (final == null)
            {
                final = DateTime.Today.Date.AddDays(7);
            }

            var parcelas = (from p in ctx.Parcela
                            join ped in ctx.Pedido on p.IdPedido equals ped.Id
                            where
                            p.Vencto >= DateTime.Today.Date
                            && p.Vencto <= final
                            && p.LibMaster == true
                            && p.IdFpgto == null
                            select p)
                            .OrderBy(x => x.Vencto)
                            .ToList();

            return parcelas;
        }

        public List<Parcela> ParcelasALiberar(DateTime? final)
        {
            if (final == null)
            {
                final = DateTime.Today.Date.AddDays(7);
            }

            var parcelas = (from p in ctx.Parcela
                            join ped in ctx.Pedido on p.IdPedido equals ped.Id
                            where
                            p.Vencto >= DateTime.Today.Date
                            && p.Vencto <= final
                            && p.LibMaster == false
                            && p.IdFpgto == null
                            select p)
                            .OrderBy(x => x.Vencto)
                            .ToList();

            return parcelas;
        }
    }
}
