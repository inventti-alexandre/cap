using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Requisicao
{
    public class ReqAutorizante
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione o autorizante")]
        [Display(Name = "Autorizante")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }

        [Display(Name = "Alterado por")]
        [Required(ErrorMessage = "Usuário inválido")]
        public int AlteradoPor { get; set; }

        [Display(Name = "Alterado em")]
        [Required]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Autorizante")]
        public virtual Usuario Autorizante
        {
            get
            {
                return new UsuarioService().Find(UsuarioId);
            }
        }

        [NotMapped]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new UsuarioService().Find(AlteradoPor);
            }
        }
    }
}
