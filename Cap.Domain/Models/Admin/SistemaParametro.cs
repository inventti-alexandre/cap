using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Admin
{
    public class SistemaParametro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o código do parâmetro")]
        [Display(Name = "Código", Prompt = "PAR_NAME_FUNC", Description = "Informar um código que lembre a que se refere o parâmetro")]
        [StringLength(100, ErrorMessage = "O código do parâmetro é composto por no máximo 100 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Informe o valor do parâmetro")]
        [StringLength(255, ErrorMessage = "O valor do parâmetro é composto por no máximo 255 caracteres")]
        [Display(Name = "Valor", Prompt = "Ex: 1, 10, true...", Description = "Valor atribuído ao parâmetro")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "Informe a descrição do parâmetro")]
        [Display(Name = "Descrição", Prompt = "Breve descrição da utilização do parâmetro e local onde será utilizado")]
        public string Descricao { get; set; }

        [Display(Name = "Ativo", Description = "Se este parâmetro será ou não utilizado pelo sistema")]
        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [Required(ErrorMessage ="Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1,double.MaxValue,ErrorMessage ="Empresa inválida")]
        public int IdEmpresa { get; set; }

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
        [Display(Name = "Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new EmpresaService().Find(IdEmpresa);
            }
            set { }
        }
    }
}