using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class FertilizanteConfig : EntityTypeConfiguration<Fertilizante>
    {
        public FertilizanteConfig()
        {
            Property(o => o.nome)
               .IsRequired()
               .HasMaxLength(80);
 
            HasOptional(o => o.Fornecedor)
                .WithMany(o=>o.Fertilizante)
                .HasForeignKey(o => o.IDFornecedor);

            HasOptional(o => o.EstagioCultura)
                .WithMany(o => o.Fertilizante)
                .HasForeignKey(o => o.IDEstagioCultura);

            HasRequired(o => o.CicloProducao)
                .WithMany(o=>o.Fertilizante)
                .HasForeignKey(o => o.IDCicloProducao);
        }
    }
}
