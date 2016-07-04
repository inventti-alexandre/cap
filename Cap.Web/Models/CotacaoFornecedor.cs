using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using System.Collections.Generic;

namespace Cap.Web.Models
{
    public class CotacaoFornecedor
    {
        public int RequisicaoId { get; set; }

        public int FornecedorId { get; set; }

        public List<CotCotacao> CotCotacao { get; set; }

        public CotDadosCotacao CotDadosCotacao { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual ReqRequisicao Requisicao { get; set; }
    }
}