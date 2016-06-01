using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Admin
{
    public class Grupo
    {
        [Key]
        public int Id { get; set; }

        [Range(1,double.MaxValue,ErrorMessage ="Selecione a empresa")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage ="Informe o nome do grupo")]
        [Display(Name ="Grupo")]
        [StringLength(40,ErrorMessage ="O nome do grupo é composto por no máximo 40 caracteres")]
        public string Descricao { get; set; }

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
        [Display(Name ="Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new EmpresaService().Find(IdEmpresa);
            }
        }
    }
}
