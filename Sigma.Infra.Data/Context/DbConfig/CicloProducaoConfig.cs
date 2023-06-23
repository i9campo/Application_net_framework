using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class CicloProducaoConfig : EntityTypeConfiguration<CicloProducao>
    {
        public CicloProducaoConfig()
        {
            Property(o => o.identificacao)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.tipo)
                .IsRequired()
                .HasMaxLength(2);

            Property(o => o.observacoes)
                .HasColumnType("text");

            Property(o => o.parametroInterno)
                .HasColumnType("text");

            Property(o => o.parametroTecnico)
                .HasColumnType("text");

            HasRequired(o => o.AreaServico)
                .WithMany(o=>o.CicloProducao)
                .HasForeignKey(o => o.IDAreaServico);

            HasOptional(o => o.Cultura)
                .WithMany(o => o.CicloProducao)
                .HasForeignKey(o => o.IDCultura);

            HasOptional(o => o.VariedadeCultura)
                .WithMany(o => o.CicloProducao)
                .HasForeignKey(o => o.IDVariedadeCultura);

            HasOptional(o => o.CulturaAnterior)
               .WithMany(o => o.CicloProducaoAnterior)
               .HasForeignKey(o => o.IDCulturaAnterior);

            Property(o => o.codigo)
                .IsOptional();

            Property(o => o.inoculante)
                .IsOptional();

            Property(o => o.jsonField)
                .IsOptional();

            Property(o => o.centerLegend)
                .IsOptional(); 
        }
    }
}
