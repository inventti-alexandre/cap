using Cap.Domain.Models.Cap;
using System.ComponentModel.DataAnnotations;

namespace Cap.Domain.Models.Requisicao
{
    public class CotCotacao
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Requisição")]
        [Range(1,double.MaxValue,ErrorMessage ="Requisição inválida")]
        public int ReqRequisicaoId { get; set; }

        [Display(Name = "Fornecedor")]
        [Range(1,double.MaxValue,ErrorMessage ="Fornecedor inválido")]
        public int FornecedorId { get; set; }

        [Display(Name = "Insumo")]
        [Range(1,double.MaxValue,ErrorMessage ="Insumo inválido")]
        public int ReqMaterialId { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage ="Informe o preço")]
        [Range(0.01, double.MaxValue,ErrorMessage ="Preço inválido")]
        public decimal Preco { get; set; }

        [Display(Name ="Observações")]
        [StringLength(60,ErrorMessage ="A observação é composta por no máximo 60 caracteres")]
        public string Observ { get; set; }

        [Display(Name ="Requisição")]
        public virtual ReqRequisicao Requisicao { get; set; }

        [Display(Name ="Fornecedor")]
        public virtual Fornecedor Fornecedor { get; set; }

        [Display(Name ="Material")]
        public virtual ReqMaterial ReqMaterial { get; set; }

    }
}
