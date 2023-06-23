
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class ProdutividadeVariedadeConfig : EntityTypeConfiguration<ProdutividadeVariedade>
    {
        public ProdutividadeVariedadeConfig()
        {
            HasRequired(o => o.Regiao)
                    .WithMany(o=>o.ProdutividadeVariedade)
                    .HasForeignKey(o => o.IDRegiao);

            HasRequired(o => o.UnidadeMedida)
                    .WithMany(o => o.ProdutividadeVariedade)
                    .HasForeignKey(o => o.IDUnidadeMedida);

            HasRequired(o => o.VariedadeCultura)
                    .WithMany(o => o.ProdutividadeVariedade)
                    .HasForeignKey(o => o.IDVariedadeCultura);

            Property(o => o.qtdeProduzida)
                .IsRequired();

            Property(o => o.ciclo)
                .IsRequired()
                .HasMaxLength(15);

            Property(o => o.publicacao)
                .HasMaxLength(30);

            Property(o => o.autor)
               .HasMaxLength(30);

            Property(o => o.condicao)
               .HasMaxLength(10);

                     
        }
    }
}
