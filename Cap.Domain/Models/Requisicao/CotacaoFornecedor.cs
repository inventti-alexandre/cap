using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Requisicao;
using System.Collections.Generic;
using System;

namespace Cap.Domain.Models.Requisicao
{
    public class CotacaoFornecedor
    {
        public int RequisicaoId { get; set; }

        public int FornecedorId { get; set; }

        public List<CotCotacao> CotCotacao { get; set; }

        public CotDadosCotacao CotDadosCotacao { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual ReqRequisicao Requisicao
        {
            get
            {
                return new ReqRequisicaoService().Find(RequisicaoId);
            }
        }
    }
}