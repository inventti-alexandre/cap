using Cap.Domain.Service.Admin;
using System.ComponentModel.DataAnnotations;

namespace Cap.Domain.Models.Admin
{
    public class TelaRegraModel
    {
        [Display(Name = "Tela")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione a tela")]
        public int IdTela { get; set; }

        [Display(Name = "Regra")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione a regra")]
        public int IdRegra { get; set; }

        [Display(Name = "Selecionado")]
        public bool Selecionado { get; set; }

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
    }
}
