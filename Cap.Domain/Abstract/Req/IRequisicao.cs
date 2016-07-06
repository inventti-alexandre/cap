using Cap.Domain.Models.Requisicao;
using System.Collections.Generic;

namespace Cap.Domain.Abstract.Req
{
    public interface IRequisicao
    {
        List<ReqRequisicao> GetRequisicoes(Situacao situacao, int idEmpresa, int idUsuario = 0);
    }
}
