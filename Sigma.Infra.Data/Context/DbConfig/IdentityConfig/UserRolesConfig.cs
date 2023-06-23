using Sigma.Domain.IdentityEntities;
using System.Data.Entity.ModelConfiguration;
namespace Sigma.Infra.Data.Context.MapConfig.IdentityConfig
{
    public class UserRolesConfig : EntityTypeConfiguration<UserRoles>
    {
        public UserRolesConfig()
        {
            HasKey(o => new { o.UserId, o.RoleId });

            Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(128);

            Property(o => o.RoleId)
                .IsRequired()
                .HasMaxLength(128);

            HasRequired(o => o.Usuario)
               .WithMany()
               .HasForeignKey(o => o.UserId);

            HasRequired(o => o.Roles)
               .WithMany()
               .HasForeignKey(o => o.RoleId);

            ToTable("AspNetUserRoles");
        }
    }
}
