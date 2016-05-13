using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Gen
{
    public class AgendaTelefoneService : IBaseService<AgendaTelefone>
    {
        private IBaseRepository<AgendaTelefone> repository;

        public AgendaTelefoneService()
        {
            repository = new EFRepository<AgendaTelefone>();
        }

        public AgendaTelefone Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                AgendaTelefone telefone = repository.Find(id);

                if (telefone != null)
                {
                    telefone.Ativo = false;
                    telefone.AlteradoEm = DateTime.Now;
                    return repository.Alterar(telefone);
                }

                return telefone;
            }
        }

        public AgendaTelefone Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(AgendaTelefone item)
        {
            item.Numero = item.Numero.ToUpper().Trim();
            item.Contato = string.IsNullOrEmpty(item.Contato) ? string.Empty : item.Contato.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Numero == item.Numero && x.IdAgenda == item.IdAgenda && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Telefone já cadastrado");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<AgendaTelefone> Listar()
        {
            return repository.Listar();
        }
    }
}
