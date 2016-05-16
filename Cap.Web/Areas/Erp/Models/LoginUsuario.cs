using System.ComponentModel.DataAnnotations;

namespace Cap.Web.Areas.Erp.Models
{
    public class LoginUsuario
    {
        [Required]
        [StringLength(100, ErrorMessage = "O email é composto por no máximo de 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20,ErrorMessage ="A senha é composta por no máximo de 20 caracteres")]
        public string Senha { get; set; }
    }
}