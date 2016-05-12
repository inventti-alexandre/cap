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
            kernel.Bind<ILogin<Agenda>>().To<AgendaService>();
            kernel.Bind<ILogin<AgendaEmail>>().To<AgendaEmailService>();
            kernel.Bind<ILogin<AgendaTelefone>>().To<AgendaTelefoneService>();
            kernel.Bind<ILogin<Banco>>().To<BancoService>();
            kernel.Bind<ILogin<Departamento>>().To<DepartamentoService>();
            kernel.Bind<ILogin<Empresa>>().To<EmpresaService>();
            kernel.Bind<ILogin<Estado>>().To<EstadoService>();
            kernel.Bind<ILogin<EstadoCivil>>().To<EstadoCivilService>();
            kernel.Bind<ILogin<Feriado>>().To<FeriadoService>();
            kernel.Bind<ILogin<FPgto>>().To<FPgtoService>();
            kernel.Bind<ILogin<Material>>().To<MaterialService>();
            kernel.Bind<ILogin<MatGrupo>>().To<MatGrupoService>();
            kernel.Bind<ILogin<ReqMaterial>>().To<ReqMaterialService>();
            kernel.Bind<ILogin<ReqRequisicao>>().To<ReqRequisicaoService>();
            kernel.Bind<ILogin<SistemaParametro>>().To<SistemaParametroService>();
            kernel.Bind<ILogin<Socio>>().To<SocioService>();
            kernel.Bind<ILogin<Unidade>>().To<UnidadeService>();
            kernel.Bind<ILogin<Usuario>>().To<UsuarioService>();

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