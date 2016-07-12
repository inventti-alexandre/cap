﻿using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using System.Collections.Generic;

namespace Cap.Domain.Abstract.Req
{
    public interface ICotadoCom
    {
        void EnviarCotacaoFornecedor(int idRequisicao, List<int> fornecedores, int idUsuario);
        bool EnviarCotacaoFornecedor(int idRequisicao, string email);
        void GravarEnvioAoFornecedor(int idRequisicao, int idFornecedor, int idUsuario);
        CotCotadoCom GetCotacaoFornecedor(int idRequisicao, int idFornecedor);
    }
}
