using Cap.Domain.Models.Requisicao;

namespace Cap.Domain.Abstract.Req
{
    public interface IReqComprar
    {
        void Comprar(ReqComprar item);
        void EnviarOrdemCompra(ReqComprar item);
        int AgendarPagamento(int idRequisicao, int idFornecedor, int idUsuario);

    }
}
