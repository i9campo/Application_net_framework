using Sigma.Domain.IdentityEntities;
using System.Data.Entity.ModelConfiguration;
namespace Sigma.Infra.Data.Context.MapConfig.IdentityConfig
{
    public class RolesConfig : EntityTypeConfiguration<Roles>
    {
        public RolesConfig()
        {
            HasKey(o => o.Id);
            Property(o => o.Id)
                .HasMaxLength(128)
                .IsRequired();

            Property(o => o.Name)
                .HasMaxLength(256)
                .IsRequired();

            Property(o => o.Tipo)
                .IsOptional();

            Property(o => o.ViewerRoler)
                .HasMaxLength(256)
                .IsRequired();

            ToTable("AspNetRoles");
        }
    }
}
