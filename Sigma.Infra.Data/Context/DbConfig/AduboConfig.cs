using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class AduboConfig : EntityTypeConfiguration<Adubo>
    {
        public AduboConfig()
        {
            Property(o => o.descricao)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
