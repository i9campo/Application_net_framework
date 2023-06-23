

using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class FornecedorConfig : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.estado)
                .IsRequired()
                .HasMaxLength(10);

            Property(o => o.cidade)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.local)
                .IsOptional()
                .HasMaxLength(100); 

        }
    }
}
