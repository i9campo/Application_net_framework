using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class PropriedadeConfig : EntityTypeConfiguration<Propriedade>
    {
        public PropriedadeConfig()
        {
            HasOptional(o => o.Regiao)
                .WithMany(o=>o.Propriedade)
                 .HasForeignKey(o => o.IDRegiao);

            HasRequired(o => o.Proprietario)
                 .WithMany(o => o.Propriedade)
                 .HasForeignKey(o => o.IDProprietario);

            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(75);

            Property(o => o.endereco)
                .HasMaxLength(100);

            Property(o => o.geo);

            Property(o => o.cidade)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.uf)
                .IsRequired()
                .HasMaxLength(2);

            Property(o => o.ie)
                .HasMaxLength(20);

            Property(o => o.fone)
                .IsRequired()
                .HasMaxLength(17);

            Property(o => o.areaTotal); 

            Property(o => o.fax)
                .HasMaxLength(17);

            Property(o => o.infoAdicionais)
                .HasMaxLength(1000);

        }
    }
}
