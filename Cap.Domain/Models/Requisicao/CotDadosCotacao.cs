using System;
using System.ComponentModel.DataAnnotations;

namespace Cap.Domain.Models.Requisicao
{
    public class CotDadosCotacao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Cotado com")]
        [Range(1,double.MaxValue,ErrorMessage ="Dados da cotação inválidos")]
        public int CotCotadoComId { get; set; }

        [Required(ErrorMessage ="Informe o contato na empresa")]
        [StringLength(60,ErrorMessage ="O contato é composto por no máximo 60 caracteres")]
        public string Contato { get; set; }

        [Required(ErrorMessage ="Informe a condição de pagamento")]
        [StringLength(40,ErrorMessage ="A condição de pagamento é composta por no máximo 40 caracteres")]
        [Display(Name ="Condição de pagamento")]
        public string Condicao { get; set; }

        [Required(ErrorMessage = "Informe a previsão de entrega")]
        [StringLength(40, ErrorMessage = "A previsão de entrega é composta por no máximo 40 caracteres")]
        [Display(Name ="Previsão de entrega")]
        public string PrevisaoEntrega { get; set; }

        [Required(ErrorMessage = "Informe a validade da proposta")]
        [StringLength(40, ErrorMessage = "A validade da proposta é composta por no máximo 20 caracteres")]
        [Display(Name ="Validade da proposta")]
        public string Validade { get; set; }

        [Display(Name ="Valor do frete")]
        [Range(0,double.MaxValue,ErrorMessage ="Valor do frete inválido")]
        public decimal Frete { get; set; }

        [Display(Name ="Impostos %")]
        [Range(0,200,ErrorMessage ="% dos impostos inválida")]
        public decimal Imposto { get; set; }

        [Display(Name ="Observações")]
        public string Observ { get; set; }

        [Display(Name ="Alterado em")]
        [Required]
        public DateTime AlteradoEm { get; set; }

        [Display(Name = "Cotado Com")]
        public virtual CotCotadoCom CotadoCom { get; set; }
    }
}
