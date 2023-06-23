using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class ImagemConfig : EntityTypeConfiguration<Imagem>
    {
        public ImagemConfig()
        {
            HasRequired(o => o.AreaServico)
                .WithMany(o=>o.Imagem)
                .HasForeignKey(o => o.IDAreaServico);

            Property(o => o.tipo)
                .HasMaxLength(20)
                .IsRequired();

            Property(o => o.image)
                .IsRequired()
                .HasColumnType("image");

            Property(o => o.indice)
                .IsRequired();

            Property(o => o.nome)
                .HasMaxLength(100)
                .IsRequired();

            Property(o => o.legenda1)
                .HasMaxLength(50)
                .IsRequired();

            Property(o => o.legenda2)
                .HasMaxLength(50)
                .IsRequired();

            Property(o => o.legenda3)
                .HasMaxLength(50)
                .IsRequired();

            Property(o => o.legenda4)
                .HasMaxLength(50)
                .IsRequired();

            Property(o => o.legenda5)
                .HasMaxLength(50)
                .IsRequired();

            Property(o => o.legenda6)
                .HasMaxLength(50)
                .IsRequired();

            Property(o => o.formatoGerado)
                .HasMaxLength(2)
                .IsRequired();

            Property(o => o.distPontos)
                .IsOptional();

            Property(o => o.perBuffer)
                .IsOptional();

            Property(o => o.perBuffer)
                .IsOptional();

            Property(o => o.potencia)
                .IsOptional();

            Property(o => o.tipoDistPontos)
                .IsOptional();
        }
    }
}
