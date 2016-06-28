using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Cap.Domain.Models.Requisicao
{
    public class CotFornecedor
    {
        [Key]
        public int Id { get; set; }

        [HiddenInput(DisplayValue =false)]
        [Range(1,double.MaxValue, ErrorMessage ="Selecione o grupo de cotação")]
        public int CotGrupoId { get; set; }

        [Display(Name ="Fornecedor")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione o fornecedor")]
        public int FornecedorId { get; set; }

        [Display(Name ="Email")]
        [Required(ErrorMessage ="Informe o email do fornecedor")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Ativo { get; set; }

        [Display(Name ="Alterado em")]
        [Required]
        public DateTime AlteradoEm { get; set; }

        [Display(Name ="Usuário")]
        [HiddenInput(DisplayValue = false)]
        public int UsuarioId { get; set; }

        [Display(Name ="Usuário")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name ="Fornecedor")]
        public virtual Fornecedor Fornecedor { get; set; }

        [Display(Name ="Grupo")]
        public virtual CotGrupo Grupo { get; set; }
    }
}
