using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cap.Web.Areas.Erp.Models
{
    public enum SituacaoPagamento
    {
        EmAberto,
        Pago
    }

    public class PesquisaModel
    {
        [Display(Name = "Departamento")]
        public int IdDepartamento { get; set; }

        [Display(Name ="Fornecedor")]
        public int IdFornecedor { get; set; }

        [Display(Name = "Nota fiscal")]
        public string NF { get; set; }

        [Display(Name = "Vencto inicial")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Inicial { get; set; }

        [Display(Name = "Vencto final")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Final { get; set; }

        [Display(Name = "Pesquisar por data de pagamento")]
        public bool PesquisarPorDataPagamento { get; set; }

        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Display(Name = "Valor maior que")]
        public bool MaiorQue { get; set; }

        [Display(Name = "Valor menor que")]
        public bool MenorQue { get; set; }

        [Display(Name = "Pagável em")]
        public int IdPgto { get; set; }

        [Display(Name = "Pago em")]
        public int IdFPgto { get; set; }

        [Display(Name = "Observações")]
        public string Observ { get; set; }

        [Display(Name = "Nosso número")]
        public string NN { get; set; }

        [Display(Name = "Pedido número")]
        public int IdPedido { get; set; }

        [Display(Name = "Banco")]
        public int IdBanco { get; set; }

        [Display(Name = "Conta")]
        public int IdConta { get; set; }

        [Display(Name = "Cheque")]
        public int Cheque { get; set; }
    }
}