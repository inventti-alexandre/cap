using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class ContaService : IBaseService<Conta>
    {
        IBaseRepository<Conta> repository;

        public ContaService()
        {
            repository = new EFRepository<Conta>();
        }

        public Conta Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Conta conta = repository.Find(id);

                if (conta != null)
                {
                    conta.AlteradoEm = DateTime.Now;
                    conta.Ativo = false;
                    return repository.Alterar(conta);
                }

                return conta;
            }
        }

        public Conta Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Conta item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.Agencia = item.Agencia.ToUpper().Trim();
            item.AgenciaNome = item.AgenciaNome.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;
            item.ContaNumero = item.ContaNumero.ToUpper().Trim();
            item.Observ = (item.Observ == null ? string.Empty : item.Observ.ToUpper().Trim());
            item.DataSaldo = (item.DataSaldo > DateTime.Today.Date ? DateTime.Today.Date : item.DataSaldo);
            item.DataSaldoAnterior = (item.DataSaldoAnterior >= item.DataSaldo ? item.DataSaldo.AddDays(-1) : item.DataSaldoAnterior);
            item.ChequeAtual = (item.ChequeAtual == 0 ? item.ChequeAtual = 1 : item.ChequeAtual);

            if (repository.Listar().Where(x => x.IdEmpresa == item.IdEmpresa && x.Descricao == item.Descricao && x.ContaNumero == item.ContaNumero && x.IdContaTipo == item.IdContaTipo && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Conta já cadastrada anteriormente");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Conta> Listar()
        {
            return repository.Listar();
        }
    }
}
