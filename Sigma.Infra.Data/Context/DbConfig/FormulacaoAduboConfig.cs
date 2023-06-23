using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class FormulacaoAduboConfig : EntityTypeConfiguration<FormulacaoAdubo>
    {
        public FormulacaoAduboConfig()
        {
            Property(o => o.descricao)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(o => o.Adubo)
                .WithMany(o=>o.FormulacaoAdubo)
                .HasForeignKey(o => o.IDAdubo);
        }
    }
}
