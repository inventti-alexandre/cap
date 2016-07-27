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

        [Display(Name ="Moeda padrão")]
        [Range(1,int.MaxValue, ErrorMessage ="Selecione a moeda padrão para agendamento das parcelas")]
        public int MoedaPadrao { get; set; }

        [Display(Name = "Forma preferencial de pagamento ao agendar parcelas")]
        [Range(1,int.MaxValue, ErrorMessage ="Informe a forma preferencial de agendamento das parcelas")]
        public int FormaTradicionalDePagamento { get; set; }

        [Display(Name = "Dias gráfico")]
        [Range(1, 31, ErrorMessage ="Valor entre 1 e 31")]
        public int GraficoDias { get; set; }
    }
}
