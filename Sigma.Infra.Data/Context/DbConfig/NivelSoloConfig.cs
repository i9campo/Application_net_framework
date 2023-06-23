using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class NivelSoloConfig : EntityTypeConfiguration<NivelSolo>
    {
        public NivelSoloConfig()
        {
            Property(o => o.elemento)
                .HasMaxLength(2);

            HasRequired(o => o.Cultura)
                .WithMany(o=>o.NivelSolo)
                .HasForeignKey(o => o.IDCultura);
        }
    }
}
