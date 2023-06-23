using System;
using Sigma.Infra.CrossCutting.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

using Microsoft.AspNet.Identity.EntityFramework;
using Sigma.Infra.CrossCutting.Identity.Context;
using System.Threading.Tasks;
using Sigma.Domain.IdentityEntities;

namespace Sigma.Infra.CrossCutting.Identity.Configuration
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));


            // Configurando validator para nome de usuario
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };


            // Logica de validação e complexidade de senha
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configuração de Lockout
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Providers de Two Factor Autentication
            //manager.RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser>
            //{
            //    MessageFormat = "Seu código de segurança é: {0}"
            //});

            //manager.RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser>
            //{
            //    Subject = "Código de Segurança",
            //    BodyFormat = "Seu código de segurança é: {0}"
            //});

            // Definindo a classe de serviço de e-mail
            //manager.EmailService = new EmailService();

            // Definindo a classe de serviço de SMS
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

        public Task RemoveClaim(string id, Claims p)
        {
            throw new NotImplementedException();
        }
    }
}