using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe o material")]
        [Display(Name ="Material")]
        [StringLength(200,ErrorMessage ="A descrição do material é composta por no máximo 200 caracteres")]
        public string Descricao { get; set; }

        [Range(1,double.MaxValue,ErrorMessage ="Selecione a unidade")]
        [Display(Name ="Unidade")]
        public int IdUnidade { get; set; }

        [Range(1, double.MaxValue,ErrorMessage ="Selecione o grupo")]
        [Display(Name = "Grupo")]
        public int IdMatGrupo { get; set; }

        [Range(0,180,ErrorMessage ="O prazo não pode ser negativo e nem superior a 180 dias")]
        [Display(Name ="Prazo mínimo para entrega")]
        public byte PrazoMinimoEntrega { get; set; }

        [Display(Name ="Estoque atual")]
        public decimal EstoqueAtual { get; set; }

        [Display(Name ="Compra automática")]
        public bool CompraAutomatica { get; set; }

        [Display(Name ="Quantidade mínima para pedido")]
        [Range(0,double.MaxValue,ErrorMessage ="A quantidade mínima não pode ser negativa")]
        public decimal QtdeMinimaPedido { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        public virtual Unidade Unidade
        {
            get
            {
                return new UnidadeService().Find(IdUnidade);
            }
            set { }
        }

        [NotMapped]
        public virtual MatGrupo MatGrupo
        {
            get
            {
                return new MatGrupoService().Find(IdMatGrupo);
            }
            set { }
        }

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
    }
}
