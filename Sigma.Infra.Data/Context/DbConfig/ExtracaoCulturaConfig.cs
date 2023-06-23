

using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class ExtracaoCulturaConfig : EntityTypeConfiguration<ExtracaoCultura>
    {
        public ExtracaoCulturaConfig()
        {
            Property(o => o.nutriente)
                .IsRequired()
                .HasMaxLength(6);

            Property(o => o.IDCultura)
                .IsRequired();

            Property(o => o.nivel1)
                .IsOptional();

            Property(o => o.nivel2)
               .IsOptional();

            Property(o => o.nivel3)
               .IsOptional();

            Property(o => o.nivel4)
               .IsOptional();

            Property(o => o.nivel5)
               .IsOptional();

            HasRequired(o => o.Cultura)
               .WithMany(o => o.ExtracaoCultura)
               .HasForeignKey(o => o.IDCultura);


        }
    }
}
