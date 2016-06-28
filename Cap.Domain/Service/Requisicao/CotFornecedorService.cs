using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class CotFornecedorService : IBaseService<CotFornecedor>
    {
        private IBaseRepository<CotFornecedor> repository;

        public CotFornecedorService()
        {
            repository = new EFRepository<CotFornecedor>();
        }

        public CotFornecedor Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                var item = repository.Find(id);

                if (item != null)
                {
                    item.Ativo = false;
                    item.AlteradoEm = DateTime.Now;
                    return repository.Alterar(item);
                }

                return item;
            }
        }

        public CotFornecedor Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(CotFornecedor item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Email = item.Email.ToLower().Trim();

            if (repository.Listar().Where(x => x.CotGrupoId == item.CotGrupoId && x.FornecedorId == item.FornecedorId && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Fornecedor já cadastrado neste grupo de cotação");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<CotFornecedor> Listar()
        {
            return repository.Listar();
        }
    }
}
