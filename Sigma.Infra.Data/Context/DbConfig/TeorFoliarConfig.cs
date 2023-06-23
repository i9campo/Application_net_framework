using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class TeorFoliarConfig : EntityTypeConfiguration<TeorFoliar>
    {
        public TeorFoliarConfig()
        {
            Property(o => o.numero)
                .IsRequired();

            HasRequired(o => o.AmostraFoliar)
                .WithMany(o=>o.TeorFoliar)
                .HasForeignKey(o => o.IDAmostraFoliar);
        }
    }
}
