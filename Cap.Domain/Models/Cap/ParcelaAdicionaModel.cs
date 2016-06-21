using Cap.Domain.Abstract;
using Cap.Domain.Service.Cap;
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
        Mensal = 1,
        Semanal = 7,
        Quinzenal = 15,
        Bimestral = 2,
        Trimestral = 3,
        Semestral = 6,
        Anual = 12,
        Nenhuma = 0
    }

    public class ParcelaAdicionaModel
    {
        [Display(Name = "Moeda")]
        [Range(1,double.MaxValue, ErrorMessage ="Selecione a moeda")]
        public int IdMoeda { get; set; }

        [Required(ErrorMessage ="Informe o valor da parcela")]
        [Range(0.01,double.MaxValue, ErrorMessage ="O valor da parcela não pode ser negativo")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Display(Name ="Forma pagamento")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione a forma de pagamento")]
        public int IdPgto { get; set; }

        [Display(Name = "Número parcelas")]
        [Range(1,300,ErrorMessage ="O número de parcelas varia entre 1 e 300 na inclusão")]
        public int Parcelas { get; set; }

        [Display(Name ="Vencimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Vencto { get; set; }

        public Periodicidade Periodicidade { get; set; }

        [Display(Name = "Observações")]
        public string Observ { get; set; }

        public virtual Pgto Pgto
        {
            get
            {
                return new PgtoService().Find(IdPgto);
            }
        }
    }
}
