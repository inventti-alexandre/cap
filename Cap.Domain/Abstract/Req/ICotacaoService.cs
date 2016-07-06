using Cap.Domain.Models.Requisicao;

namespace Cap.Domain.Abstract.Req
{
    public interface ICotacaoService
    {
        void GravarCotacao(CotacaoFornecedor cotacao);

        CotacaoFornecedor GetCotacao(int idRequisicao, int idFornecedor);
    }
}
