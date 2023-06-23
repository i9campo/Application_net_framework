using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class EstagioCulturaConfig : EntityTypeConfiguration<EstagioCultura>
    {
        public EstagioCulturaConfig()
        {
            Property(o => o.descricao)
                .IsRequired()
                .HasMaxLength(70);

            Property(o => o.codigo)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.detalhamento)
                .HasMaxLength(4000);

            HasRequired(o => o.Cultura)
                .WithMany(o=>o.EstagioCultura)
                .HasForeignKey(o => o.IDCultura);
        }
    }
}
