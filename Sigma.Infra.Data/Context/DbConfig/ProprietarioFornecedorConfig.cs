using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class ProprietarioFornecedorConfig : EntityTypeConfiguration<ProprietarioFornecedor>
    {
        public ProprietarioFornecedorConfig()
        {
            HasRequired(o => o.Proprietario)
                   .WithMany(o=>o.ProprietarioFornecedor)
                   .HasForeignKey(o => o.IDProprietario);

            HasRequired(o => o.Fornecedor)
                   .WithMany(o => o.ProprietarioFornecedor)
                   .HasForeignKey(o => o.IDFornecedor);
        }
    }
}
