using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Requisicao
{
    public class ReqComprar
    {
        [Display(Name ="Requisição")]
        [Required(ErrorMessage ="Requisição inválida")]
        public ReqRequisicao Requisicao { get; set; }

        [Display(Name ="Fornecedor")]
        [Required(ErrorMessage ="Selecione o fornecedor")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione o fornecedor")]
        public int FornecedorId { get; set; }

        [Display(Name = "Data prevista para entrega")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage ="Informe a data prevista para entrega")]
        public DateTime PrevisaoEntrega { get; set; }

        [Display(Name ="Entrega noturna?")]
        public bool EntregaNoturna { get; set; }

        [Display(Name ="Agendar logística da empresa para retirar o material no fornecedor?")]
        public bool AgendarLogistica { get; set; }

        [Display(Name ="Enviar ordem de compra ao fornecedor?")]
        public bool EnviarOrdemCompra { get; set; }

        [Display(Name ="Comprada por")]
        [Required]
        public int CompradaPor { get; set; }

        [Display(Name ="Parcelas")]
        [Required(ErrorMessage ="Nenhuma parcela prevista para pagamento foi agendada")]
        public List<Parcela> Parcela { get; set; }

        [NotMapped]
        [Display(Name = "Fornecedor")]
        public virtual Fornecedor Fornecedor
        {
            get
            {
                return new FornecedorService().Find(FornecedorId);
            }
        }
    }
}