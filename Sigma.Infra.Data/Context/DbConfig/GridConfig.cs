using Sigma.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class GridConfig : EntityTypeConfiguration<Grid>
    {
        public GridConfig()
        {
            HasRequired(o => o.AreaServico)
                .WithMany(o=>o.Grid)
                .HasForeignKey(o => o.IDAreaServico);

            Property(o => o.descricao)
                .HasMaxLength(50)
                .IsRequired();

            Property(o => o.tamanho)
                .IsRequired();

            Property(o => o.jsonField)
                .IsRequired()
                .HasColumnType("text");

            Property(o => o.jsonField)
               .IsOptional(); 

            Property(o => o.codigo)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
               .IsRequired();
        }
    }
}
