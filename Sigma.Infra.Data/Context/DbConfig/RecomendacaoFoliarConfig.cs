using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;


namespace Sigma.Infra.Data.MapConfig
{
    public class RecomendacaoFoliarConfig : EntityTypeConfiguration<RecomendacaoFoliar>
    {
        public RecomendacaoFoliarConfig()
        {
            Property(o => o.descritivo)
                .HasMaxLength(500);

            Property(o => o.codigo)
                .IsRequired()
                .HasMaxLength(26);

            Property(o => o.elemento)
                .HasColumnType("char")
                .HasMaxLength(2);

            Property(o => o.excecao)
                .IsRequired();

            HasRequired(o => o.Cultura)
                .WithMany(o=>o.RecomendacaoFoliar)
                .HasForeignKey(o => o.IDCultura);

        }
    }
}
