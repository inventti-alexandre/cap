using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Cap
{
    public enum Periodicidade
    {
        Mensal = 30,
        Semanal = 7,
        Quinzenal = 15,
        Bimestral = 60,
        Trimestral = 90,
        Semetral = 180,
        Anual = 365,
        Nenhuma = 0
    }

    public class ParcelaAdicionaModel
    {
        [Display(Name = "Moeda")]
        [Range(1,double.MaxValue, ErrorMessage ="Selecione a moeda")]
        public int IdMoeda { get; set; }

        [Required(ErrorMessage ="Informe o valor da parcela")]
        [Range(0.01,double.MaxValue, ErrorMessage ="O valor da parcela não pode ser negativo")]
        public decimal Valor { get; set; }

        [Display(Name ="Forma pagamento")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione a forma de pagamento")]
        public int IdPgto { get; set; }

        [Display(Name = "Número parcelas")]
        [Range(1,300,ErrorMessage ="O número de parcelas varia entre 1 e 300 na inclusão")]
        public int Parcelas { get; set; }

        [Display(Name ="Vencimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Vencto { get; set; }

        public Periodicidade Periodicidade { get; set; }

        [Display(Name = "Observações")]
        public string Observ { get; set; }
    }
}
