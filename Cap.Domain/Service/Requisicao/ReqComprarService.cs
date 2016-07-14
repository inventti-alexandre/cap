using Cap.Domain.Abstract.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using Cap.Domain.Abstract;

namespace Cap.Domain.Service.Requisicao
{
    public class ReqComprarService : IReqComprar
    {
        private IBaseService<Pedido> servicePedido;
        private IBaseService<Parcela> serviceParcela;
        private IBaseService<ReqRequisicao> serviceRequisicao;

        public ReqComprarService()
        {
            this.servicePedido = new PedidoService();
            this.serviceParcela = new ParcelaService();
            this.serviceRequisicao = new ReqRequisicaoService();
        }

        public void Comprar(ReqComprar item)
        {
            try
            {
                if (item.PrevisaoEntrega < DateTime.Today.Date)
                {
                    throw new ArgumentException("Previsão de entrega inválida");
                }

                if (item.Parcela.Count() == 0)
                {
                    throw new ArgumentException("Informe ao menos uma parcela para pagamento");
                }

                // grava pedido
                item.Requisicao.PedidoId = gravarPedido(item);

                // grava parcelas
                item = gravarParcelas(item);

                // atualiza situacao da requisicao
                item.Requisicao.Situacao = Situacao.Comprada;
                serviceRequisicao.Gravar(item.Requisicao);

                // parei aki
                // agenda logistica
                if (item.AgendarLogistica == true)
                {
                    // TODO: agendar logistica
                }

                // envia ordem de compra ao fornecedor
                if (item.EnviarOrdemCompra == true)
                {
                    // TODO: enviar ordem de compra
                }

            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        private int? gravarPedido(ReqComprar item)
        {
            var pedido = new Pedido
            {
                AlteradoPor = item.CompradaPor,
                CriadoEm = DateTime.Now,
                CriadoPor = item.CompradaPor,
                IdCentroCusto = item.Requisicao.CentroCustoId,
                IdDepartamento = item.Requisicao.IdDepartamento,
                IdEmpresa = item.Requisicao.Departamento.IdEmpresa,
                IdFornecedor = item.FornecedorId,
                IdGrupoCusto = item.Requisicao.GrupoCustoId
            };

            return servicePedido.Gravar(pedido);
        }

        private ReqComprar gravarParcelas(ReqComprar item)
        {
            for (int i = 0; i < item.Parcela.Count(); i++)
            {
                item.Parcela[i].IdPedido = (int)item.Requisicao.PedidoId;
                item.Parcela[i].Id = serviceParcela.Gravar(item.Parcela[i]);
            }

            return item;
        }
    }
}
