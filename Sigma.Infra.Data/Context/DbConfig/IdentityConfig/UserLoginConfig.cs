using Sigma.Domain.IdentityEntities;
using System.Data.Entity.ModelConfiguration;
namespace Sigma.Infra.Data.Context.MapConfig.IdentityConfig
{
    public class UserLoginConfig : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginConfig()
        {
            HasKey(o => new { o.LoginProvider, o.ProviderKey, o.UserId });

            Property(o => o.LoginProvider)
                .IsRequired()
                .HasMaxLength(128);

            Property(o => o.ProviderKey)
                .IsRequired()
                .HasMaxLength(128);

            Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(128);

            HasRequired(o => o.Usuario)
               .WithMany()
               .HasForeignKey(o => o.UserId);

            ToTable("AspNetUserLogins");
        }
    }
}
