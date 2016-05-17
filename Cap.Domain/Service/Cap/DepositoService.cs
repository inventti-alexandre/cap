using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class DepositoService : IBaseService<Deposito>
    {
        private IBaseRepository<Deposito> repository;

        public DepositoService()
        {
            repository = new EFRepository<Deposito>();
        }

        public Deposito Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Deposito deposito = repository.Find(id);

                if (deposito != null)
                {
                    deposito.Ativo = false;
                    deposito.AlteradoEm = DateTime.Now;
                    return repository.Alterar(deposito);
                }

                return deposito;
            }
        }

        public Deposito Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Deposito item)
        {
            item.Favorecido = item.Favorecido.ToUpper().Trim();
            item.Agencia = item.Agencia.ToUpper().Trim();
            item.Conta = item.Conta.ToUpper().Trim();
            item.Cpf = string.IsNullOrEmpty(item.Cpf) ? "" : item.Cpf.ToUpper().Trim();
            item.Observ = string.IsNullOrEmpty(item.Observ) ? "" : item.Observ.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.IdEmpresa == item.IdEmpresa && x.Favorecido == item.Favorecido && x.Conta == item.Conta && x.Agencia == item.Agencia && x.Poupanca == item.Poupanca && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um favorecido cadastrado nesta conta e agência");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Deposito> Listar()
        {
            return repository.Listar();
        }
    }
}
