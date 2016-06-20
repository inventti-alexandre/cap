using Cap.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Cap.Domain.Models.Admin
{
    public class TrocaSenha
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Informe a senha atual do usuário")]
        [StringLength(20, ErrorMessage = "A senha é composta por no mínimo 6 caracteres e no máximo 20 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha anterior")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Informe a nova senha do usuário")]
        [StringLength(20, ErrorMessage = "A senha é composta por no mínimo 6 caracteres e no máximo 20 caracteres", MinimumLength = 6)]
        [Display(Name = "Nova senha")]
        [NotEqualTo("SenhaAtual", ErrorMessage = "A senha anterior e atual não devem ser as mesmas")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem")]
        [Required(ErrorMessage = "Informe a nova senha do usuário")]
        [StringLength(20, ErrorMessage = "A senha é composta por no mínimo 6 caracteres e no máximo 20 caracteres", MinimumLength = 6)]
        [Display(Name = "Confirmação", Description = "Repita a nova senha digitada")]
        [DataType(DataType.Password)]
        public string NovaSenhaConfere { get; set; }
    }
}
