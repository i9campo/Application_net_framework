using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class FaixaTeorConfig : EntityTypeConfiguration<FaixaTeor>
    {
        public FaixaTeorConfig()
        {
            Property(o => o.nutriente)
                .IsRequired()
                .HasMaxLength(2);

            HasRequired(o => o.EstagioCultura)
                .WithMany(o=>o.FaixaTeor)
                .HasForeignKey(o => o.IDEstagioCultura);

            HasRequired(o => o.PartePlanta)
                .WithMany(o => o.FaixaTeor)
                .HasForeignKey(o => o.IDPartePlanta);
        }
    }
}
