using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class CulturaConfig : EntityTypeConfiguration<Cultura>
    {
        public CulturaConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.nitrogenio)
                .IsRequired();

            Property(o => o.ciclo)
                .IsRequired();

            Property(o => o.decomposicao)
                .IsRequired();

            Property(o => o.nSimb)
                .IsRequired();

            Property(o => o.nSimbMedida)
                .IsRequired();

            HasRequired(o => o.UnidadeMedida)
                .WithMany(o=>o.Cultura)
                .HasForeignKey(o => o.IDUnidadeMedida);
        }
    }
}
