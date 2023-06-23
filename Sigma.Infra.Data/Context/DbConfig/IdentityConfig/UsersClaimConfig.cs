using Sigma.Domain.IdentityEntities;
using System.Data.Entity.ModelConfiguration;
namespace Sigma.Infra.Data.Context.MapConfig.IdentityConfig
{
    public class UsersClaimConfig : EntityTypeConfiguration<UserClaims>
    {
        public UsersClaimConfig()
        {
            HasKey(o => o.Id);

            Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(128);

            HasRequired(o => o.Usuario)
                .WithMany()
                .HasForeignKey(o => o.UserId);

            ToTable("AspNetUserClaims");
        }
    }
}
