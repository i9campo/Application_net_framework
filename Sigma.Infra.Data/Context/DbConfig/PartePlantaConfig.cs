using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class PartePlantaConfig : EntityTypeConfiguration<PartePlanta>
    {
        public PartePlantaConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(30);

            HasRequired(o => o.Cultura)
                .WithMany(o=>o.PartePlanta)
                .HasForeignKey(o => o.IDCultura);
        }
    }
}
