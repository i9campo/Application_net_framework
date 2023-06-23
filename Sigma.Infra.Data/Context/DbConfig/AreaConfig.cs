using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigma.Infra.Data.MapConfig
{
    public class AreaConfig : EntityTypeConfiguration<Area>
    {
        public AreaConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.anoAbertura)
                .IsOptional();

            Property(o => o.altitudeMedia)
                .IsOptional();

            Property(o => o.area_geo)
                .IsOptional(); 

            Property(o => o.tipoPredSolo)
                .HasMaxLength(50);

            Property(o => o.codigo)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            .IsRequired();

            HasRequired(o => o.Propriedade)
                 .WithMany(o=>o.Area)
                 .HasForeignKey(o => o.IDPropriedade);
        }
    }
}
