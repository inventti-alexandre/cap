using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class FornecedorService : IBaseService<Fornecedor>
    {
        private IBaseRepository<Fornecedor> repository;

        public FornecedorService()
        {
            repository = new EFRepository<Fornecedor>();
        }

        public Fornecedor Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Fornecedor fornecedor = repository.Find(id);

                if (fornecedor != null)
                {
                    fornecedor.Ativo = false;
                    fornecedor.AlteradoEm = DateTime.Now;
                    return repository.Alterar(fornecedor);
                }

                return fornecedor;
            }
        }

        public Fornecedor Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Fornecedor item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Fantasia = item.Fantasia.ToUpper().Trim();
            item.Razao = item.Razao == null ? string.Empty : item.Razao.ToUpper().Trim();
            item.Contato = item.Contato == null ? string.Empty : item.Contato.ToUpper().Trim();
            item.Observ = item.Observ == null ? string.Empty : item.Observ.ToUpper().Trim();
            item.CNPJ = item.CNPJ == null ? string.Empty : item.CNPJ.ToUpper().Trim();
            item.IE = item.IE == null ? string.Empty : item.IE.ToUpper().Trim();

            if (repository.Listar().Where(x => x.IdEmpresa == item.IdEmpresa && x.Fantasia == item.Fantasia && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um fornecedor cadastrado com este nome fantasia");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Fornecedor> Listar()
        {
            return repository.Listar();
        }
    }
}
