using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;


namespace Sigma.Infra.Data.MapConfig
{
    public class UnidadeMedidaConfig : EntityTypeConfiguration<UnidadeMedida>
    {
        public UnidadeMedidaConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(o => o.peso)
                .IsRequired();
        }
    }
}
