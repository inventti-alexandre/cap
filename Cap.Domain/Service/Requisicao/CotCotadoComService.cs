using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;
using System.Collections.Generic;
using Cap.Domain.Abstract.Email;
using Cap.Domain.Service.Email;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using System.Text;
using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;

namespace Cap.Domain.Service.Requisicao
{
    public class CotCotadoComService : IBaseService<CotCotadoCom>, ICotadoCom
    {
        private IBaseRepository<CotCotadoCom> repository;
        private IEmail serviceEmail;
        private IBaseService<CotFornecedor> serviceCotFornecedor;
        private IBaseService<ReqRequisicao> serviceRequisicao;
        private IBaseService<SistemaParametro> serviceParamentro;

        public CotCotadoComService()
        {
            repository = new EFRepository<CotCotadoCom>();
            serviceEmail = new EnviarEmail();
            serviceCotFornecedor = new CotFornecedorService();
            serviceParamentro = new SistemaParametroService();
            serviceRequisicao = new ReqRequisicaoService();
        }

        public CotCotadoCom Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);

            }
        }

        public CotCotadoCom Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(CotCotadoCom item)
        {
            item.AlteradoEm = DateTime.Now;

            if (item.Id == 0)
            {
                item.Preenchida = false;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<CotCotadoCom> Listar()
        {
            return repository.Listar();
        }

        public CotCotadoCom GetCotacaoFornecedor(int idRequisicao, int idFornecedor)
        {
            return repository.Listar()
                .Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor)
                .FirstOrDefault();                
        }

        public void EnviarCotacaoFornecedor(int idRequisicao, List<int> fornecedores, int idUsuario)
        {
            string assunto = $"Cotação de preços id: {idRequisicao}";

            foreach (var item in fornecedores)
            {
                var fornecedor = serviceCotFornecedor.Find(item);

                if (fornecedor != null)
                {
                    // envia email
                    if (serviceEmail.Enviar(fornecedor.Fornecedor.Fantasia, fornecedor.Email, assunto, getHtmlCotacao(idRequisicao, item)) == true)
                    {
                        // grava
                        CotCotadoCom cotcom = GetCotacaoFornecedor(idRequisicao, item);
                        if (cotcom == null)
                        {
                            cotcom = new CotCotadoCom
                            {
                                AlteradoEm = DateTime.Now,
                                FornecedorId = item,
                                Preenchida = false,
                                ReqRequisicaoId = idRequisicao,
                                UsuarioId = idUsuario
                            };
                        }
                        else
                        {
                            cotcom.AlteradoEm = DateTime.Now;
                        }
                        Gravar(cotcom);
                    }
                }
            }
        }

        public string getHtmlCotacao(int idRequisicao, int idFornecedor)
        {
            var requisicao = serviceRequisicao.Find(idRequisicao);
            var departamento = requisicao.Departamento;
            string codigoParamentro = "LINK_COTACAO";
            string url = serviceParamentro.Listar()
                .Where(x => x.Codigo == codigoParamentro)
                .FirstOrDefault()
                .Valor;

            string link = string.Format("{0}?idRequisicao={1}&idFornecedor={2}", url, idRequisicao, idFornecedor);

            StringBuilder sb = new StringBuilder();
            sb.Append("<h2>COTAÇÃO DE PREÇOS</h2>")
                .Append($"Departamento: { departamento.Descricao} <br />{ departamento.Endereco }, { departamento.Bairro }, { departamento.Cidade }, CEP { departamento.Cep }<br />")
                .Append($"Previsão de entrega: {requisicao.EntregarDia.ToShortDateString() } <br />Solicitado por { requisicao.SolicitadoPor.Nome } <br />")
                .Append("<h3>ITENS A COTAR</h3>")
                .Append("<table>")
                .Append("<tr><th>")
                .Append("<td>QUANTIDADE</td>")
                .Append("<td>UNIDADE</td>")
                .Append("<td>MATERIAL</td>")
                .Append("<td>ESPECIFICACOES</td>");

            foreach (var item in requisicao.ReqMaterial.ToList())
            {
                sb.Append("<tr>")
                    .Append($"<td>{ item.Qtde.ToString("n2") }</td>")
                    .Append($"<td>{ item.Material.Unidade.Descricao }</td>")
                    .Append($"<td>{ item.Material.Descricao }</td>")
                    .Append($"<td>{ item.Observ }</td>")
                    .Append("</tr>");
            }

            sb.Append("</tr></th>")
                .Append("</table>");

            sb.Append("<br />")
                .Append("<h4><a href='{link}' _target='_blank'>CLIQUE PARA RESPONDER ESTA COTAÇÃO</a></h4>");

            return sb.ToString();
        }
    }
}
