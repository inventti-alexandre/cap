using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Gen;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
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
            kernel.Bind<IBaseService<Departamento>>().To<DepartamentoService>();
            kernel.Bind<IBaseService<Deposito>>().To<DepositoService>();
            kernel.Bind<IBaseService<Empresa>>().To<EmpresaService>();
            kernel.Bind<IBaseService<Estado>>().To<EstadoService>();
            kernel.Bind<IBaseService<EstadoCivil>>().To<EstadoCivilService>();
            kernel.Bind<IBaseService<Feriado>>().To<FeriadoService>();
            kernel.Bind<IBaseService<FPgto>>().To<FPgtoService>();
            kernel.Bind<IBaseService<Material>>().To<MaterialService>();
            kernel.Bind<IBaseService<MatGrupo>>().To<MatGrupoService>();
            kernel.Bind<IBaseService<Moeda>>().To<MoedaService>();
            kernel.Bind<IBaseService<Pgto>>().To<PgtoService>();
            kernel.Bind<IBaseService<ReqMaterial>>().To<ReqMaterialService>();
            kernel.Bind<IBaseService<ReqRequisicao>>().To<ReqRequisicaoService>();
            kernel.Bind<IBaseService<SistemaParametro>>().To<SistemaParametroService>();
            kernel.Bind<IBaseService<Socio>>().To<SocioService>();
            kernel.Bind<IBaseService<Unidade>>().To<UnidadeService>();
            kernel.Bind<IBaseService<Usuario>>().To<UsuarioService>();

            kernel.Bind<ITrocaSenha>().To<UsuarioService>();
            kernel.Bind<ILogin>().To<UsuarioService>();

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