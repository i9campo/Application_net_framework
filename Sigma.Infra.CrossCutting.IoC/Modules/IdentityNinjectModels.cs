using Ninject.Modules;
using Sigma.Infra.CrossCutting.Identity.Configuration;
using Sigma.Infra.CrossCutting.Identity.Context;


namespace Sigma.Infra.CrossCutting.IoC.Modules
{
    public class IdentityNinjectModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ApplicationDbContext>().ToSelf();
            Bind<ApplicationRoleManager>().ToSelf();
            Bind<ApplicationUserManager>().ToSelf();
            Bind<ApplicationSignInManager>().ToSelf();

            //Bind(typeof(IUserStore<>)).To(typeof(UserStore<>));
            //Bind(typeof(IUserStore<>)).To<UserStore<ApplicationUser>>();
        }
    }
}
