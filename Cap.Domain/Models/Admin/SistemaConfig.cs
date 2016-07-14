using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Admin
{
    public class SistemaConfig
    {
        public int IdEmpresa { get; set; }

        [Display(Name ="Exibir requisições compradas dos últimos dias")]
        [Range(1,30,ErrorMessage ="O valor varia entre 1 e 30 dias, para outros períodos utilize a pesquisa")]
        public int RequisicaoExibirComprasUltimosDias { get; set; }

        [Display(Name = "Exibir requisições compradas com entregas previstas nos próximos dias")]
        [Range(1,30,ErrorMessage ="O valor varia entre 1 e 30 dias, para outros períodos utilize a pesquisa")]
        public int RequisicaoExibirEntregasPrevistasAteProximosDias { get; set; }
    }
}
