using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Service.Cap
{
    public class ContaFinanceiraService : IBaseService<ContaFinanceira>
    {
        private IBaseRepository<ContaFinanceira> repository;

        public ContaFinanceiraService()
        {
            repository = new EFRepository<ContaFinanceira>();
        }

        public ContaFinanceira Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                var conta = repository.Find(id);

                if (conta != null)
                {
                    conta.Ativo = false;
                    conta.AlteradoEm = DateTime.Now;
                    return repository.Alterar(conta);
                }

                return conta;
            }
        }

        public ContaFinanceira Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(ContaFinanceira item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.IdEmpresa == item.IdEmpresa && x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma conta financeira cadastrada com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<ContaFinanceira> Listar()
        {
            return repository.Listar();
        }
    }
}
