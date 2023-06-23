using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class ProblemaConfig : EntityTypeConfiguration<Problema>
    {
        public ProblemaConfig()
        {
            Property(o => o.tipo)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.descricao)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.ano)
                .IsRequired();

            Property(o => o.nivel)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(o => o.Area)
               .WithMany(o => o.Problema)
               .HasForeignKey(o => o.IDArea);

        }
    }
}
