using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Service.Gen
{
    public class AgendaEmailService : ILogin<AgendaEmail>
    {
        private IBaseRepository<AgendaEmail> repository;

        public AgendaEmailService()
        {
            repository = new EFRepository<AgendaEmail>();
        }

        public AgendaEmail Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                throw new ArgumentException("Email inexistente");
            }
        }

        public AgendaEmail Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(AgendaEmail item)
        {
            item.Email = item.Email.ToLower().Trim();
            item.Contato = string.IsNullOrEmpty(item.Contato) ? string.Empty : item.Contato.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Email == item.Email && x.IdAgenda == item.IdAgenda && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Email já cadastrado");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<AgendaEmail> Listar()
        {
            return repository.Listar();
        }
    }
}
