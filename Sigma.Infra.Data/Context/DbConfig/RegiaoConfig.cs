using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;


namespace Sigma.Infra.Data.MapConfig
{
    public class RegiaoConfig : EntityTypeConfiguration<Regiao>
    {
        public RegiaoConfig()
        {
            Property(o => o.descricao)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
