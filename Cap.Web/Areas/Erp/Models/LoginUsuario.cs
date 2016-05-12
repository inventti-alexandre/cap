using System.ComponentModel.DataAnnotations;

namespace Cap.Web.Areas.Erp.Models
{
    public class LoginUsuario
    {
        [Required]
        [StringLength(20, ErrorMessage = "O login é composto por no máximo de 20 caracteres")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20,ErrorMessage ="A senha é composta por no máximo de 20 caracteres")]
        public string Senha { get; set; }
    }
}