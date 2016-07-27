using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class SistemaConfigService: ISistemaConfig
    {
        private IBaseService<SistemaParametro> service;
        const string _requisicaoExibirComprasUltimosDias = "REQ_EXIB_COMP_ULT_DIAS";
        const string _requisicaoExibirEntregasPrevistasAteProximosDias = "REQ_EXIB_ENT_PREV_ATE_PROX_DIAS";
        const string _moedaPadrao = "MOEDA_PADRAO";
        const string _pgtoPreferencial = "PGTO_PREF";
        const string _graficoDias = "GRAFICO_DIAS";

        public SistemaConfigService()
        {
            this.service = new SistemaParametroService();
        }

        public SistemaConfig GetConfig(int idEmpresa)
        {
            string valor;

            var item = new SistemaConfig();
            item.IdEmpresa = idEmpresa;

            // requisicoes compradas nos ultimos N dias
            valor = getParametro(_requisicaoExibirComprasUltimosDias, idEmpresa);
            item.RequisicaoExibirComprasUltimosDias = string.IsNullOrEmpty(valor) ? 5 : Convert.ToInt32(valor);

            // requisicoes compradas com entregas previstas e nao confirmadas proximos N dias
            valor = getParametro(_requisicaoExibirEntregasPrevistasAteProximosDias, idEmpresa);
            item.RequisicaoExibirEntregasPrevistasAteProximosDias = string.IsNullOrEmpty(valor) ? 3 : Convert.ToInt32(valor);

            // moeda padrao
            valor = getParametro(_moedaPadrao, idEmpresa);
            item.MoedaPadrao = string.IsNullOrEmpty(valor) ? 0 : Convert.ToInt32(valor);

            // forma preferencial de pagamento
            valor = getParametro(_pgtoPreferencial, idEmpresa);
            item.FormaTradicionalDePagamento = string.IsNullOrEmpty(valor) ? 0 : Convert.ToInt32(valor);

            // grafico dias
            valor = getParametro(_graficoDias, idEmpresa);
            item.GraficoDias = string.IsNullOrEmpty(valor) ? 30 : Convert.ToInt32(valor);

            return item;
        }

        public void SetConfig(SistemaConfig config, int idUsuario)
        {
            setParametro(_requisicaoExibirComprasUltimosDias, config.IdEmpresa, idUsuario, config.RequisicaoExibirComprasUltimosDias.ToString());
            setParametro(_requisicaoExibirEntregasPrevistasAteProximosDias, config.IdEmpresa, idUsuario, config.RequisicaoExibirEntregasPrevistasAteProximosDias.ToString());
            setParametro(_moedaPadrao, config.IdEmpresa, idUsuario, config.MoedaPadrao.ToString());
            setParametro(_pgtoPreferencial, config.IdEmpresa, idUsuario, config.FormaTradicionalDePagamento.ToString());
            setParametro(_graficoDias, config.IdEmpresa, idUsuario, config.GraficoDias.ToString());
        }

        private string getParametro(string codigo, int idEmpresa)
        {
            var parametro = service.Listar().Where(x => x.IdEmpresa == idEmpresa && x.Codigo == codigo).FirstOrDefault();

            if (parametro == null)
            {
                return string.Empty;
            }

            return parametro.Valor;
        }

        private void setParametro(string codigo, int idEmpresa, int idUsuario, string valor)
        {
            var parametro = service.Listar().Where(x => x.IdEmpresa == idEmpresa && x.Codigo == codigo).FirstOrDefault();

            if (parametro == null)
            {
                parametro = new SistemaParametro
                {
                    AlteradoEm = DateTime.Now,
                    AlteradoPor = idUsuario,
                    Codigo = codigo,
                    Descricao = codigo,
                    IdEmpresa = idEmpresa,
                    Valor = valor
                };
            }
            else
            {
                parametro.Valor = valor;
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = idUsuario;
            }

            service.Gravar(parametro);
        }
    }
}
