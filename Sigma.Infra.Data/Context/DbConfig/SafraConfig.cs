using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;


namespace Sigma.Infra.Data.MapConfig
{
    public class SafraConfig : EntityTypeConfiguration<Safra>
    {
        public SafraConfig()
        {
            Property(o => o.descricao)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
