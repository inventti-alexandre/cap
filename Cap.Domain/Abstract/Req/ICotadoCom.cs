using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Abstract.Req
{
    public interface ICotadoCom
    {
        void EnviarCotacaoFornecedor(int idRequisicao, List<int> fornecedores, int idUsuario);

        CotCotadoCom GetCotacaoFornecedor(int idRequisicao, int idFornecedor);
    }
}
