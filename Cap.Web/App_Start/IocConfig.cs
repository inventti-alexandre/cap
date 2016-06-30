using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Abstract.Gen;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Email;
using Cap.Domain.Models.Gen;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using Cap.Domain.Service.Email;
using Cap.Domain.Service.Gen;
using Cap.Domain.Service.Requisicao;
using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Cap.Web.App_Start
{
    public class IocConfig
    {
        public static void ConfigurarDependencias()
        {
            // container
            IKernel kernel = new StandardKernel();

            // mapeamento - interfaces x classes
            kernel.Bind<IBaseService<Agenda>>().To<AgendaService>();
            kernel.Bind<IBaseService<AgendaEmail>>().To<AgendaEmailService>();
            kernel.Bind<IBaseService<AgendaTelefone>>().To<AgendaTelefoneService>();
            kernel.Bind<IBaseService<Banco>>().To<BancoService>();
            kernel.Bind<IBaseService<CentroCusto>>().To<CentroCustoService>();
            kernel.Bind<IBaseService<CentroLucro>>().To<CentroLucroService>();
            kernel.Bind<IBaseService<Conta>>().To<ContaService>();
            kernel.Bind<IBaseService<ContaFinanceira>>().To<ContaFinanceiraService>();
            kernel.Bind<IBaseService<ContaTipo>>().To<ContaTipoService>();
            kernel.Bind<IBaseService<CotCotacao>>().To<CotCotacaoService>();
            kernel.Bind<IBaseService<CotCotadoCom>>().To<CotCotadoComService>();
            kernel.Bind<IBaseService<CotDadosCotacao>>().To<CotDadosCotacaoService>();
            kernel.Bind<IBaseService<CotFornecedor>>().To<CotFornecedorService>();
            kernel.Bind<IBaseService<CotGrupo>>().To<CotGrupoService>();
            kernel.Bind<IBaseService<Departamento>>().To<DepartamentoService>();
            kernel.Bind<IBaseService<Deposito>>().To<DepositoService>();
            kernel.Bind<IBaseService<EmailConfig>>().To<EmailConfigService>();
            kernel.Bind<IBaseService<Empresa>>().To<EmpresaService>();
            kernel.Bind<IBaseService<Estado>>().To<EstadoService>();
            kernel.Bind<IBaseService<EstadoCivil>>().To<EstadoCivilService>();
            kernel.Bind<IBaseService<Feriado>>().To<FeriadoService>();
            kernel.Bind<IBaseService<Fornecedor>>().To<FornecedorService>();
            kernel.Bind<IBaseService<FPgto>>().To<FPgtoService>();
            kernel.Bind<IBaseService<Grupo>>().To<GrupoService>();
            kernel.Bind<IBaseService<GrupoCusto>>().To<GrupoCustoService>();
            kernel.Bind<IBaseService<GrupoFinanceiro>>().To<GrupoFinanceiroService>();
            kernel.Bind<IBaseService<GrupoLucro>>().To<GrupoLucroService>();
            kernel.Bind<IBaseService<Indice>>().To<IndiceService>();
            kernel.Bind<IBaseService<IndVariacao>>().To<IndVariacaoService>();
            kernel.Bind<IBaseService<Logistica>>().To<LogisticaService>();
            kernel.Bind<IBaseService<Material>>().To<MaterialService>();
            kernel.Bind<IBaseService<MatGrupo>>().To<MatGrupoService>();
            kernel.Bind<IBaseService<Moeda>>().To<MoedaService>();
            kernel.Bind<IBaseService<Motorista>>().To<MotoristaService>();
            kernel.Bind<IBaseService<Parcela>>().To<ParcelaService>();
            kernel.Bind<IBaseService<Pedido>>().To<PedidoService>();
            kernel.Bind<IBaseService<Pgto>>().To<PgtoService>();
            kernel.Bind<IBaseService<RegimeTributario>>().To<RegimeTributarioService>();
            kernel.Bind<IBaseService<ReqMaterial>>().To<ReqMaterialService>();
            kernel.Bind<IBaseService<ReqRequisicao>>().To<ReqRequisicaoService>();
            kernel.Bind<IBaseService<SistemaArea>>().To<SistemaAreaService>();
            kernel.Bind<IBaseService<SistemaParametro>>().To<SistemaParametroService>();
            kernel.Bind<IBaseService<SistemaRegra>>().To<SistemaRegraService>();
            kernel.Bind<IBaseService<SistemaTela>>().To<SistemaTelaService>();
            kernel.Bind<IBaseService<Socio>>().To<SocioService>();
            kernel.Bind<IBaseService<TelaRegra>>().To<TelaRegraService>();            
            kernel.Bind<IBaseService<Unidade>>().To<UnidadeService>();
            kernel.Bind<IBaseService<Usuario>>().To<UsuarioService>();

            kernel.Bind<ICotadoCom>().To<CotCotadoComService>();
            kernel.Bind<IIndVariacaoCalculo>().To<IndVariacaoService>();
            kernel.Bind<ILiberacao>().To<LiberacaoService>();
            kernel.Bind<ILiberacaoPagamento>().To<LiberacaoPagamentoService>();
            kernel.Bind<ILogin>().To<UsuarioService>();
            kernel.Bind<ILogistica>().To<LogisticaService>();
            kernel.Bind<ITelaRegra>().To<TelaRegraService>();
            kernel.Bind<ITrocaSenha>().To<UsuarioService>();
            kernel.Bind<IUsuarioRegra>().To<UsuarioService>();

            // registro das dependencias
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyResolver(IResolutionRoot kernel)
        {
            _resolutionRoot = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _resolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }
}