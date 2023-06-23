using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class LaboratorioConfig : EntityTypeConfiguration<Laboratorio>
    {
        public LaboratorioConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.cnpj)
                .IsRequired()
                .HasMaxLength(20);

            Property(o => o.endereco)
                .IsRequired()
                .HasMaxLength(70);

            Property(o => o.cep)
                 .HasMaxLength(10);

            Property(o => o.telefone)
                .HasMaxLength(14);
        }
    }
}
