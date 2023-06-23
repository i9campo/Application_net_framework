using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class TeorSoloConfig : EntityTypeConfiguration<TeorSolo>
    {
        public TeorSoloConfig()
        {
            HasRequired(o => o.AmostraFoliar)
                .WithMany(o=>o.TeorSolo)
                .HasForeignKey(o => o.IDAmostraFoliar);
        }
    }
}
