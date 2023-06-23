using Sigma.Domain.IdentityEntities;
using System.Data.Entity.ModelConfiguration;
namespace Sigma.Infra.Data.Context.IdentityConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(o => o.Id);

            Property(o => o.Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(o => o.UserName)
                .IsRequired()
                .HasMaxLength(256);

            Property(o => o.Email)
              .IsRequired()
              .HasMaxLength(256);

            Property(o => o.EmailConfirmed)
                .IsRequired()
                .HasColumnType("bit");

            Property(o => o.PasswordHash)
                .IsOptional();

            Property(o => o.SecurityStamp)
                .IsRequired();

            Property(o => o.PhoneNumber)
                .IsOptional();

            Property(o => o.PhoneNumberConfirmed)
                .IsOptional()
                .HasColumnType("bit");

            Property(o => o.AccessFailedCount)
                .IsRequired();

            Property(o => o.TwoFactorEnabled)
                .IsRequired()
                .HasColumnType("bit"); 

            Property(o => o.LockoutEndDateUtc)
                .IsOptional(); 

            Property(o => o.LockoutEnabled)
                .IsRequired();


            Property(o => o.AccessFailedCount)
                .IsRequired(); 

            ToTable("AspNetUsers"); 
        }
    }
}
