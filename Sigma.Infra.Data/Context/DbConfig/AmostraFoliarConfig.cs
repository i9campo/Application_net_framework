using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class AmostraFoliarConfig : EntityTypeConfiguration<AmostraFoliar>
    {
        public AmostraFoliarConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(20);

            Property(o => o.data)
                .IsRequired();

            HasRequired(o => o.AreaServico)
                    .WithMany(o=>o.AmostraFoliar)
                    .HasForeignKey(o => o.IDAreaServico);

            HasRequired(o => o.EstagioCultura)
                    .WithMany(o=>o.AmostraFoliar)
                    .HasForeignKey(o => o.IDEstagioCultura);

            HasRequired(o => o.PartePlanta)
                    .WithMany(o=>o.AmostraFoliar)
                    .HasForeignKey(o => o.IDPartePlanta);
        }
    }
}
