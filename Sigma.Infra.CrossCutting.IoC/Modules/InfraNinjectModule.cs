using Ninject.Modules;
using Sigma.Infra.Data.Context;
using Sigma.Infra.Data.Context.DbConfig;

namespace Sigma.Infra.CrossCutting.IoC.Modules
{
    public class InfraNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IDbContext)).To(typeof(DBContext));
        }
    }
}