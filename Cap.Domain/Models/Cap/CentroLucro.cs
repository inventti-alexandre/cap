using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class CentroLucro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Grupo de lucro inválido")]
        [Display(Name = "Grupo de lucro")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione o grupo de lucro")]
        public int IdGrupoLucro { get; set; }

        [Required(ErrorMessage = "Informe a descrição do centro de lucro")]
        [Display(Name = "Grupo de custo")]
        [StringLength(40, ErrorMessage = "O centro de lucro é composto por no máximo 40 caracteres")]
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
        [Display(Name = "Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new EmpresaService().Find(IdEmpresa);
            }
        }

        [NotMapped]
        public virtual GrupoLucro GrupoLucro
        {
            get
            {
                return new GrupoLucroService().Find(IdGrupoLucro);
            }
        }
    }
}