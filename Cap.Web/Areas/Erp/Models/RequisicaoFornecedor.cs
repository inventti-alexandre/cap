using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;

namespace Cap.Web.Areas.Erp.Models
{
    public class RequisicaoFornecedor
    {
        public ReqRequisicao Requisicao { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}