using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Email;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public CotCotadoCom GetCotacaoFornecedor(string guid)
        {
            return repository.Listar()
                .Where(x => x.Guid == guid)
                .FirstOrDefault();
        }

        public void EnviarCotacaoFornecedor(int idRequisicao, List<int> fornecedores, int idUsuario)
        {
            var requisicao = serviceRequisicao.Find(idRequisicao);
            var departamento = requisicao.Departamento;
            string assunto = getAssunto(idRequisicao, departamento);
            string guid;

            foreach (var item in fornecedores)
            {
                var fornecedor = serviceCotFornecedor.Find(item);

                if (fornecedor != null)
                {
                    // envia email
                    guid = new Guid().ToString();
                    if (serviceEmail.Enviar(fornecedor.Fornecedor.Fantasia, fornecedor.Email, assunto, getHtmlCotacao(guid, item, requisicao, departamento), departamento.IdEmpresa, true) == true)
                    {
                        // grava envio ao fornecedor
                        GravarEnvioAoFornecedor(idRequisicao, fornecedor.FornecedorId, idUsuario, guid);
                    }
                }
            }
        }

        public void GravarEnvioAoFornecedor(int idRequisicao, int idFornecedor, int idUsuario, string guid)
        {
            CotCotadoCom cotcom = GetCotacaoFornecedor(idRequisicao, idFornecedor);
            if (cotcom == null)
            {
                cotcom = new CotCotadoCom
                {
                    AlteradoEm = DateTime.Now,
                    FornecedorId = idFornecedor,
                    Preenchida = false,
                    ReqRequisicaoId = idRequisicao,
                    UsuarioId = idUsuario,
                    Guid = guid
                };
            }
            else
            {
                cotcom.AlteradoEm = DateTime.Now;
            }
            Gravar(cotcom);
        }

        public bool  EnviarCotacaoFornecedor(int idRequisicao, string email)
        {
            var requisicao = serviceRequisicao.Find(idRequisicao);
            var departamento = requisicao.Departamento;
            string assunto = getAssunto(idRequisicao, departamento);

            return serviceEmail.Enviar("", email, assunto, getHtmlCotacao(new Guid().ToString(), 0, requisicao, departamento), departamento.IdEmpresa, true);
        }

        private string getAssunto(int idRequisicao, Departamento departamento)
        {
            return $"COTAÇÃO DE PREÇOS { departamento.Empresa.Fantasia } - {departamento.Descricao}, ID: {idRequisicao}";
        }

        private string getHtmlCotacao(string guid, int idFornecedor, ReqRequisicao requisicao, Departamento departamento)
        {
            string codigoParamentro = "LINK_COTACAO";
            string url = serviceParamentro.Listar()
                .Where(x => x.Codigo == codigoParamentro)
                .FirstOrDefault()
                .Valor;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head></head><body>")
                .Append("<h2>COTAÇÃO DE PREÇOS</h2>")
                .Append($"Empresa: { departamento.Empresa.Razao }<br />")
                .Append($"Departamento: { departamento.Descricao} - { departamento.Endereco }, { departamento.Bairro }, { departamento.Cidade }, CEP { departamento.Cep }<br />")
                .Append($"Previsão de entrega: {requisicao.EntregarDia.ToShortDateString() } <br />Solicitado por: { requisicao.SolicitadoPor.Nome } <br />")
                .Append("<h3>ITENS A COTAR</h3>")
                .Append("<table style='border 1px solid #C6C6C6;'>")
                .Append("<thead><tr>")
                .Append("<th>QUANTIDADE</th>")
                .Append("<th>UNIDADE</th>")
                .Append("<th>MATERIAL</th>")
                .Append("<th>ESPECIFICACOES</th>")
                .Append("</tr></thead>");

            sb.Append("<tbody>");
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
                .Append("</tbody>")
                .Append("</table>");

            if (idFornecedor != 0)
            {
                string link = $"{url}/{guid}";
                sb.Append("<br />")
                    .Append($"<h4><a href='{link}' _target='_blank'>CLIQUE PARA RESPONDER ESTA COTAÇÃO</a></h4>")
                    .Append("</body></html>");
            }

            var empresa = departamento.Empresa;

            sb.Append("<hr>")
                .Append($"{empresa.Razao}<br />{empresa.Endereco}, {empresa.Bairro}, {empresa.Cidade}, {empresa.Estado.UF}, CEP {empresa.Cep}");

            return sb.ToString();
        }
    }
}
