using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class TipoAmostraConfig : EntityTypeConfiguration<TipoAmostra>
    {
        public TipoAmostraConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(100); 
        }
    }
}
