using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class ServicoConfig : EntityTypeConfiguration<Servico>
    {
        public ServicoConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(20);

            Property(o => o.descricao)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.tipoTaxa)
                .IsRequired()
                .HasMaxLength(8);
        }
    }
}
