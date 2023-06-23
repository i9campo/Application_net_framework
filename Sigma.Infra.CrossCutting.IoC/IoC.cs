using CommonServiceLocator.NinjectAdapter.Unofficial;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Sigma.Infra.CrossCutting.IoC.Modules;

namespace Sigma.Infra.CrossCutting.IoC
{
    public class IoC
    {
        public IKernel Kernel { get; private set; }

        public IoC()
        {
            Kernel = GetNinjectModules(); 
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(Kernel));
        }

        public static StandardKernel GetNinjectModules()
        {
            return new StandardKernel(
                new ServiceNinjectModule(),
                new IdentityNinjectModules(),
                new InfraNinjectModule(),
                new RepositoryNinjectModule(),
                new AppNinjectModule()
            );
        }


    }
}
