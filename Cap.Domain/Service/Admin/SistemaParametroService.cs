using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class SistemaParametroService : IBaseService<SistemaParametro>
    {
        private IBaseRepository<SistemaParametro> repository;

        public SistemaParametroService()
        {
            repository = new EFRepository<SistemaParametro>();
        }

        public SistemaParametro Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var parametro = repository.Find(id);

                if (parametro != null)
                {
                    parametro.AlteradoEm = DateTime.Now;
                    parametro.Ativo = false;
                    repository.Alterar(parametro);
                }

                return parametro;
            }
        }

        public SistemaParametro Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(SistemaParametro item)
        {
            // formata
            item.Codigo = item.Codigo.ToUpper().Trim();
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Codigo == item.Codigo && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um parâmetro cadastrado com este código");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<SistemaParametro> Listar()
        {
            return repository.Listar();
        }

        public int HttpContext { get; set; }
    }
}