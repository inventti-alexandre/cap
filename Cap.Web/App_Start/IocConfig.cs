using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
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
            kernel.Bind<IBaseService<Banco>>().To<BancoService>();
            kernel.Bind<IBaseService<Estado>>().To<EstadoService>();
            kernel.Bind<IBaseService<EstadoCivil>>().To<EstadoCivilService>();
            kernel.Bind<IBaseService<FPgto>>().To<FPgtoService>();
            kernel.Bind<IBaseService<SistemaParametro>>().To<SistemaParametroService>();
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