using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Requisicao;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Requisicao
{
    public class ReqMaterial
    {
        [Key]
        public int Id { get; set; }

        [Range(1,Double.MaxValue,ErrorMessage ="Requisição inválida")]
        [Display(Name ="Requisição")]
        public int IdReqRequisicao { get; set; }

        [Range(1,double.MaxValue,ErrorMessage ="Material inválido")]
        [Display(Name ="Material")]
        public int IdMaterial { get; set; }

        [Range(0.0001,double.MaxValue,ErrorMessage ="A quantidade não pode ser menor ou igual a zero")]
        [Display(Name ="Quantidade")]
        public decimal Qtde { get; set; }

        [Display(Name = "Observações")]
        [StringLength(200,ErrorMessage ="A observaçõa é composta por no máximo 200 caracteres")]
        public string Observ { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new UsuarioService().Find(AlteradoPor);
            }
            set { }
        }

        [NotMapped]
        [Display(Name ="Material")]
        public virtual Material Material
        {
            get
            {
                return new MaterialService().Find(IdMaterial);
            }
            set { }
        }
    }
}
