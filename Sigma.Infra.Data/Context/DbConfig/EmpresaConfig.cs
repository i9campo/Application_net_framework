using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class EmpresaConfig : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.cnpj)
                .HasMaxLength(18);

            Property(o => o.email)
                .HasMaxLength(30);

            Property(o => o.site)
                .HasMaxLength(150);
            
            Property(o => o.fone)
                .HasMaxLength(18);
        }
    }
}
