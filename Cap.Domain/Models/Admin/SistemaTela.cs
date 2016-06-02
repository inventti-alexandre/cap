using Cap.Domain.Service.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Admin
{
    public class SistemaTela
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Texto no menu")]
        [Required(ErrorMessage ="Informe o texto do menu")]
        [StringLength(100,ErrorMessage ="O texto no menu é composto por no máximo 100 caracteres")]
        public string TextoMenu { get; set; }

        [Display(Name ="Link")]
        [Required(ErrorMessage ="Informe o link")]
        [StringLength(100, ErrorMessage ="O link é composto por no máximo 100 caracteres")]
        public string Link { get; set; }

        [Display(Name = "Breve descrição sobre a tela")]
        [Required(ErrorMessage ="Informe uma breve descrição sobre a tela do sistema")]
        [StringLength(100, ErrorMessage ="A breve descrição sobre a tela é composta por no máximo 100 caracteres")]
        public string Descricao { get; set; }

        [Display(Name ="Área do sistema")]
        [Range(1,double.MaxValue, ErrorMessage ="Selecione a área do sistema")]
        public int IdSistemaArea { get; set; }


        [Display(Name = "Regra")]
        [Range(1, double.MaxValue, ErrorMessage ="Selecione a regra")]
        public int IdSistemaRegra { get; set; }

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
        }

        [NotMapped]
        [Display(Name = "Área")]
        public virtual SistemaArea SistemaArea
        {
            get
            {
                return new SistemaAreaService().Find(IdSistemaArea);
            }
        }

        [NotMapped]
        [Display(Name = "Regra")]
        public virtual SistemaRegra SistemaRegra
        {
            get
            {
                return new SistemaRegraService().Find(IdSistemaArea);
            }
        }
    }
}
