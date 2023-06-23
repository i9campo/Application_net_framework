using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class VariedadeCulturaConfig : EntityTypeConfiguration<VariedadeCultura>
    {
        public VariedadeCulturaConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.empresaDetentora)
               .IsOptional()
               .HasMaxLength(30);

            Property(o => o.acamamento)
               .IsOptional()
               .HasMaxLength(10);

            Property(o => o.exigencia)
               .IsOptional()
               .HasMaxLength(30);

            HasRequired(o => o.Cultura)
                .WithMany(o => o.VariedadeCultura)
                .HasForeignKey(o => o.IDCultura);
        }
    }
}
