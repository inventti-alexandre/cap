using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Gen
{
    public class AgendaService : IBaseService<Agenda>
    {
        private IBaseRepository<Agenda> repository;

        public AgendaService()
        {
            repository = new EFRepository<Agenda>();
        }

        public Agenda Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Agenda agenda = repository.Find(id);

                if (agenda != null)
                {
                    agenda.Ativo = false;
                    agenda.AlteradoEm = DateTime.Now;
                    return repository.Alterar(agenda);
                }

                return agenda;
            }
        }

        public Agenda Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Agenda item)
        {
            item.Nome = item.Nome.ToUpper().Trim();
            item.Contato = string.IsNullOrEmpty(item.Contato) ? string.Empty : item.Contato.ToUpper().Trim();
            item.Endereco = string.IsNullOrEmpty(item.Endereco) ? string.Empty : item.Endereco.ToUpper().Trim();
            item.Bairro = string.IsNullOrEmpty(item.Bairro) ? string.Empty : item.Bairro.ToUpper().Trim();
            item.Cidade = string.IsNullOrEmpty(item.Cidade) ? string.Empty : item.Cidade.ToUpper().Trim();
            item.Cep = string.IsNullOrEmpty(item.Cep) ? string.Empty : item.Cep.ToUpper().Trim();
            item.WebSite = string.IsNullOrEmpty(item.WebSite) ? string.Empty : item.WebSite.ToLower().Trim();
            item.Observ = string.IsNullOrEmpty(item.Observ) ? string.Empty : item.Observ;
            item.AlteradoEm = DateTime.Now;

            //if (repository.Listar().Where(x => x.Nome == item.Nome && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            //{
            //    throw new ArgumentException("Nome já cadastrado");
            //}

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Agenda> Listar()
        {
            return repository.Listar();
        }
    }
}
