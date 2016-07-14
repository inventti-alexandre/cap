using System;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using System.Collections.Generic;

namespace Cap.Domain.Abstract.Req
{
    public interface IRequisicao
    {
        List<ReqRequisicao> GetRequisicoes(Situacao situacao, int idEmpresa, int idUsuario = 0, DateTime? inicial = null, DateTime? final = null);
        List<ReqRequisicao> GetEntregas(DateTime data, int idEmpresa, int idUsuario = 0);
        void SendToLogistica(Logistica logistica, int idRequisicao);
        void ConfirmarEntrega(int id, int idUsuario);
    }
}
