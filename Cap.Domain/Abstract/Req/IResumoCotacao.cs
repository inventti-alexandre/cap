using Cap.Domain.Models.Requisicao;

namespace Cap.Domain.Abstract.Req
{
    public interface IResumoCotacao
    {
        Resumo GetResumo(int idRequisicao);
    }
}
