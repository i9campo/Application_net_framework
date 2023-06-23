using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class ImagemSateliteConfig : EntityTypeConfiguration<ImagemSatelite>
    {
        public ImagemSateliteConfig()
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
