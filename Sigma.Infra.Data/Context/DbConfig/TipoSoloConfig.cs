using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class TipoSoloConfig : EntityTypeConfiguration<TipoSolo>
    {
        public TipoSoloConfig()
        {
            Property(o => o.abreviacao)
                .HasMaxLength(5)
                .IsRequired();

            Property(o => o.descricao)
                .HasMaxLength(20)
                .IsRequired(); 
        }
    }
}
