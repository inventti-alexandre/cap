using Cap.Domain.Service.Admin;
using System.ComponentModel.DataAnnotations;

namespace Cap.Domain.Models.Admin
{
    public class TelaRegra
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Tela")]
        [Range(1,double.MaxValue, ErrorMessage ="Selecione a tela")]
        public int IdTela { get; set; }

        [Display(Name ="Regra")]
        [Range(1,double.MaxValue, ErrorMessage ="Selecione a regra")]
        public int IdRegra { get; set; }

        public virtual SistemaTela SistemaTela
        {
            get
            {
                return new SistemaTelaService().Find(IdTela);
            }
        }

        public virtual SistemaRegra SistemaRegra
        {
            get
            {
                return new SistemaRegraService().Find(IdRegra);
            }
        }

        public virtual string Role
        {
            get
            {
                return string.Format("{0}-{1}", SistemaTela.Regra, SistemaRegra.Sufixo);
            }
        }
    }
}
