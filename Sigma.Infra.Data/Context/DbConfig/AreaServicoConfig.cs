using Sigma.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace Sigma.Infra.Data.MapConfig
{
    public class AreaServicoConfig : EntityTypeConfiguration<AreaServico>
    {
        public AreaServicoConfig()
        {
            HasRequired(o => o.Area)
                .WithMany(o=>o.AreaServico)
                .HasForeignKey(o => o.IDArea);

            HasRequired(o => o.Safra)
                .WithMany(o=>o.AreaServico)
                .HasForeignKey(o => o.IDSafra);

            HasRequired(o => o.Servico)
                .WithMany(o=>o.AreaServico)
                .HasForeignKey(o => o.IDServico);

            HasOptional(o => o.Cultura)
                .WithMany(o=> o.AreaServico)
                .HasForeignKey(o => o.IDCultura);

            HasOptional(o => o.ProprietarioFatura)
                .WithMany(o => o.AreaServico)
                .HasForeignKey(o => o.IDProprietarioFatura);

            Property(o => o.numServico)
                .IsOptional();

            Property(o => o.parametroTecnico)
                .IsOptional()
                .HasColumnType("text");

            Property(o => o.parametroInterno)
                .IsOptional()
                .HasColumnType("text");

            Property(o => o.codigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(o => o.resumoOperacional)
                .IsOptional()
                .HasColumnType("text");

            Property(o => o.contrato)
                .IsOptional();

            Property(o => o.jsonField)
                .IsOptional();

            Property(o => o.centerLegend)
                .IsOptional();


        }

    }
}
