using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Requisicao
{
    public class CotGrupo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        public int EmpresaId { get; set; }

        [Display(Name = "Grupo")]
        [Required(ErrorMessage = "Informe o nome do grupo")]
        [StringLength(60, ErrorMessage = "O nome do grupo é composto por no máximo 60 caracteres")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Alterado em")]
        [Required]
        public DateTime AlteradoEm { get; set; }

        public int UsuarioId { get; set; }

        public virtual Empresa Empresa { get; set; }

        [Display(Name ="Alterado por")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<CotFornecedor> CotarCom { get; set; }
    }
}
