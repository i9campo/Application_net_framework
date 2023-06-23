using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sigma.Infra.CrossCutting.Identity.Model
{
    public class ApplicationUser : IdentityUser
    {
        //public Guid IDEmpresa { get; set; }
        //public bool Ativo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var oClaim = await manager.CreateIdentityAsync(this, authenticationType);
            return oClaim;
        }
    }
}