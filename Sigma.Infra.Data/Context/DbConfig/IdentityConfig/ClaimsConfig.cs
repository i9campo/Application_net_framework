using Sigma.Domain.IdentityEntities;
using System.Data.Entity.ModelConfiguration;
namespace Sigma.Infra.Data.Context.IdentityConfig
{
    public class ClaimsConfig : EntityTypeConfiguration<Claims>
    {
        public ClaimsConfig()
        {
            Property(o => o.ClaimType)
                .IsRequired();

            Property(o => o.ClaimValue)
                .IsRequired(); 
        }
    }
}
