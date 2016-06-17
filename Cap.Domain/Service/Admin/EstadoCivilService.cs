using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class EstadoCivilService : IBaseService<EstadoCivil>
    {
        IBaseRepository<EstadoCivil> repository;

        public EstadoCivilService()
        {
            repository = new EFRepository<EstadoCivil>();
        }


        public EstadoCivil Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                EstadoCivil estado = repository.Find(id);

                if (estado != null)
                {
                    estado.Ativo = false;
                    estado.AlteradoEm = DateTime.Now;
                    return repository.Alterar(estado);
                }

                return estado;
            }
        }

        public EstadoCivil Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(EstadoCivil item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um estado civil cadastrado com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<EstadoCivil> Listar()
        {
            return repository.Listar();
        }
    }
}
