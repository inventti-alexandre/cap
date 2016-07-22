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
using Cap.Domain.Abstract.Email;
using Cap.Domain.Service.Email;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Service.Admin;

namespace Cap.Domain.Service.Requisicao
{
    public class ReqComprarService : IReqComprar
    {
        private IBaseService<Pedido> servicePedido;
        private IBaseService<Parcela> serviceParcela;
        private IBaseService<ReqRequisicao> serviceRequisicao;
        private IBaseService<CotCotacao> serviceCotCom;
        private IRequisicao serviceRequisicaoLogistica;
        private IEmail serviceEmail;
        private ISistemaConfig config;

        public ReqComprarService()
        {
            this.servicePedido = new PedidoService();
            this.serviceParcela = new ParcelaService();
            this.serviceRequisicao = new ReqRequisicaoService();
            this.serviceRequisicaoLogistica = new ReqRequisicaoService();
            this.serviceCotCom = new CotCotacaoService();
            serviceEmail = new EnviarEmail();
            config = new SistemaConfigService();
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

                // agenda logistica
                if (item.AgendarLogistica == true)
                {
                    serviceRequisicaoLogistica.SendToLogistica(new Logistica
                    {
                        ConcluidoObserv = string.Empty,
                        DataServico =
                        item.Requisicao.EntregarDia,
                        EmpresaId = item.Requisicao.Departamento.IdEmpresa,
                        Observ = string.Empty,
                        MotoristaId = 0,
                        UsuarioId = item.Requisicao.IdSolicitadoPor,
                        Servico = serviceRequisicaoLogistica.GetStringServico(item.Requisicao, item.FornecedorId)
                    }, item.Requisicao.Id);
                }

                // envia ordem de compra ao fornecedor
                if (item.EnviarOrdemCompra == true)
                {
                    EnviarOrdemCompra(item);
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

        public void EnviarOrdemCompra(ReqComprar item)
        {
            var requisicao = item.Requisicao;
            var departamento = requisicao.Departamento;
            var fornecedor = item.Fornecedor;
            var agenda = fornecedor.Agenda;
            var empresa = departamento.Empresa;
            var cotadoCom = item.Requisicao.CotadoCom.Where(x => x.ReqRequisicaoId == requisicao.Id).FirstOrDefault();
            var dadosCotacao = cotadoCom.DadosCotacao;
            var cotacao = dadosCotacao.CotadoCom.Cotacao;

            StringBuilder sb = new StringBuilder();
            sb.Append("<h2>ORDEM DE COMPRA</h2>");

            // empresa
            sb.AppendLine($"<br/><br/><strong>{ empresa.Razao }</strong>")
                .AppendLine($"<br />{ agenda.Endereco }, { agenda.Bairro }, { agenda.Cidade }, { agenda.Estado.UF }, CEP { agenda.Cep }")
                .AppendLine($"<br />{ agenda?.Emails?.FirstOrDefault()?.Email }, Tel. { agenda?.Telefones?.FirstOrDefault()?.Numero }");

            // fornecedor
            sb.AppendLine("<br/><br/À")
                .AppendLine($"<br/>{fornecedor.Fantasia} - {fornecedor.Razao}");

            // itens da requisicao
            sb.AppendLine("Favor providenciar os materiais abaixo relacionados:")
                .AppendLine("<table>")
                .AppendLine("<tr>")
                .AppendLine("<td>QUANTIDADE</td>")
                .AppendLine("<td>UNIDADE</td>")
                .AppendLine("<td>INSUMO</td>")
                .AppendLine("<td>PREÇO UNITÁRIO</td>")
                .AppendLine("</tr>");
            
            foreach (var material in requisicao.ReqMaterial)
            {
                var preco = cotacao.Where(x => x.ReqMaterialId == material.Id).FirstOrDefault().PrecoComImpostos;

                sb.AppendLine("<tr>")
                    .AppendLine($"<td>{ material.Qtde.ToString("N2") }</td>")
                    .AppendLine($"<td>{ material.Material.Unidade.Descricao }</td>")
                    .AppendLine($"<td>{ material.Material.Descricao }</td>")
                    .AppendLine($"<td>{ material.Material.Unidade.Descricao }</td>")
                    .AppendLine($"<td>{ preco.ToString("c2") }</td>")
                    .AppendLine("</tr>");                
            }
            // valor total da cotacao
            var totalCotacao = cotacao.Sum(x => x.PrecoComImpostos);

            sb.AppendLine("<tr>")
                .AppendLine("<td colspan='4'>VALOR TOTAL</td>")
                .AppendLine($"<td>{ totalCotacao }</td>")
                .AppendLine("<tr>");
            sb.AppendLine("</table>");

            // condicoes de pagamento
            sb.AppendLine("<h4>Condições de pagamento</h4>")
                .AppendLine($"Valor total do pedido { totalCotacao.ToString("c2")} conforme negociação, na seguinte condição de pagamento:")
                .AppendLine("<br/>");
            // TODO: listar condicoes de pagamento

            // faturamento
            sb.AppendLine("<h4>Faturamento</h4>")
                .AppendLine($"{empresa.Razao}<br>Local de cobrança: {empresa.Endereco}, {empresa.Bairro}, {empresa.Cidade}, {empresa.Estado.UF}, CEP {empresa.Cep }")
                .AppendLine($"CNPJ: {empresa.Cnpj}, Inscrição Estadual: {empresa.IE }");

            // entrega
            sb.AppendLine("<h4>ENTREGA</h4>")
                .AppendLine($"{departamento.Endereco}, {departamento.Bairro},{ departamento.Cidade}, { departamento.Estado.UF}, CEP {departamento.Cep}")
                .AppendLine($"<br />Entregar dia { requisicao.EntregarDia.ToShortDateString() }");

            // footer
            sb.AppendLine("<hr>")
                .AppendLine($"Esta ordem de compra foi gerada por { item.CompradaPorUsuario}")
                .AppendLine($"<br>Dúvidas sobre esta ordem, gentileza entrar em contato com { item.CompradaPorUsuario.Email }")
                .AppendLine($"<br>Ordem de compra #{item.Requisicao.Id} emitida em { DateTime.Now.ToString()}");

            // TODO: email do fornecedor
            serviceEmail.Enviar(dadosCotacao.Contato, agenda.Emails.First().Email, $"ORDEM DE COMPRA {requisicao.Id}", sb.ToString(), empresa.Id, true);
        }

        public int AgendarPagamento(int idRequisicao, int idFornecedor, int idUsuario)
        {
            try
            {
                var requisicao = serviceRequisicao.Find(idRequisicao);
                var valorCotacao = serviceCotCom.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).ToList().Sum(x => x.PrecoComImpostos);

                // gera pedido
                int idPedido = servicePedido.Gravar(new Pedido
                {
                    AlteradoEm = DateTime.Now,
                    AlteradoPor = idUsuario,
                    Ativo = true,
                    CriadoEm = DateTime.Now,
                    CriadoPor = idUsuario,
                    IdCentroCusto = requisicao.CentroCustoId,
                    IdDepartamento = requisicao.IdDepartamento,
                    IdEmpresa = requisicao.Departamento.IdEmpresa,
                    IdFornecedor = idFornecedor,
                    IdGrupoCusto = requisicao.GrupoCustoId,
                });

                var padrao = config.GetConfig(requisicao.Departamento.IdEmpresa);

                // grava uma parcela
                int idParcela = serviceParcela.Gravar(new Parcela
                {
                    AlteradoEm = DateTime.Now,
                    AlteradoPor = idUsuario,
                    Ativo = true,
                    CriadoEm = DateTime.Now,
                    CriadoPor = idUsuario,
                    IdEmpresa = requisicao.Departamento.IdEmpresa,
                    IdMoeda = padrao.MoedaPadrao,
                    IdPedido = idPedido,
                     IdPgto = padrao.FormaTradicionalDePagamento,
                    Valor = valorCotacao,
                    Vencto = DateTime.Today.Date.AddDays(30)
                });

                // atualiza requisicao
                requisicao.Situacao = Situacao.Comprada;
                requisicao.PedidoId = idPedido;
                requisicao.CompradoEm = DateTime.Now;
                serviceRequisicao.Gravar(requisicao);

                // retorna idPedido
                return idPedido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
