using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class ReqMaterialService : IBaseService<ReqMaterial>
    {
        private IBaseRepository<ReqMaterial> repository;

        public ReqMaterial Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                ReqMaterial reqMat = repository.Find(id);

                if (reqMat != null)
                {
                    reqMat.Ativo = false;
                    reqMat.AlteradoEm = DateTime.Now;
                }

                return reqMat;
            }
        }

        public ReqMaterial Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(ReqMaterial item)
        {
            item.Observ = string.IsNullOrEmpty(item.Observ) ? string.Empty : item.Observ.ToUpper().Trim();

            if (repository.Listar().Where(x => x.IdMaterial == item.IdMaterial && x.IdReqRequisicao == item.IdReqRequisicao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Material já cadastrado nesta requisição");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<ReqMaterial> Listar()
        {
            return repository.Listar();
        }
    }
}
