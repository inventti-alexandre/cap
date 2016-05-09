using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class EstadoService : IBaseService<Estado>
    {
        IBaseRepository<Estado> repository;

        public EstadoService()
        {
            repository = new EFRepository<Estado>();
        }

        public Estado Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Estado estado = repository.Find(id);

                if (estado != null)
                {
                    estado.Ativo = false;
                    repository.Alterar(estado);
                }

                return estado;
            }
        }

        public Estado Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Estado item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.UF = item.UF.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.UF == item.UF && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um Estado cadastrado com esta UF");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Estado> Listar()
        {
            return repository.Listar();
        }
    }
}
