using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.Context.DbConfig
{
    public class ImagemSateliteRecortadaConfig : EntityTypeConfiguration<ImagemSateliteRecortada>
    {
        public ImagemSateliteRecortadaConfig()
        {
            Property(o => o.dateIMG)
                .IsRequired()
                .HasMaxLength(11);

            Property(o => o.banda)
                .IsRequired()
                .HasMaxLength(10);

            Property(o => o.satelite)
                .IsRequired()
                .HasMaxLength(10);

            Property(o => o.visualizar)
                .IsRequired();
        }
    }
}
