using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class ReqAutorizanteService : IBaseService<ReqAutorizante>
    {
        private IBaseRepository<ReqAutorizante> repository;

        public ReqAutorizanteService()
        {
            repository = new EFRepository<ReqAutorizante>();
        }

        public ReqAutorizante Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ReqAutorizante Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(ReqAutorizante item)
        {
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.EmpresaId == item.EmpresaId && x.UsuarioId == item.UsuarioId && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Autorizante já cadastrado");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<ReqAutorizante> Listar()
        {
            return repository.Listar();
        }
    }
}
