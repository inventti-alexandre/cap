using System;
using System.ComponentModel.DataAnnotations;

namespace Cap.Web.Areas.Erp.Models
{
    public class RequisicaoParcela
    {
        [Required(ErrorMessage = "Informe a data de vencimento da parcela")]
        [Display(Name = "Vencimento")]
        [DataType(DataType.Date)]
        public DateTime Vencto { get; set; }

        [Required(ErrorMessage = "Informe o valor da parcela")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor da parcela inválido")]
        public decimal Valor { get; set; }

        [Display(Name = "Pagável em")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe a forma prevista de pagamento")]
        public int IdPgto { get; set; }

        [Display(Name = "Observações")]
        public string Observ { get; set; }
    }
}