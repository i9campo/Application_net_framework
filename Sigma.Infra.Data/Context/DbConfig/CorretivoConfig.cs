using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class CorretivoConfig : EntityTypeConfiguration<Corretivo>
    {
        public CorretivoConfig()
        {
            HasRequired(o => o.AreaServico)
                .WithMany(o=>o.Corretivo)
                .HasForeignKey(o => o.IDAreaServico);

            HasOptional(o => o.Grid)
                .WithMany(o => o.Corretivo)
                .HasForeignKey(o => o.IDGrid);

            HasOptional(o => o.Fornecedor)
                .WithMany(o => o.Corretivo)
                .HasForeignKey(o => o.IDFornecedor);

            Property(o => o.corretivo)
                .IsRequired()
                .HasColumnType("bit"); 


            Property(o => o.descricao)
                .IsRequired();
        }
    }
}
