using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class ProdutoConfig : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(80);

            Property(o => o.ativo)
                .IsRequired();

            Property(o => o.tipo)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.alvo)
                .HasMaxLength(50)
                .IsOptional();

            Property(o => o.classe)
                .HasMaxLength(30);

            Property(o => o.formato)
                .HasMaxLength(40); 


            HasRequired(o => o.Fornecedor)
                .WithMany(o=>o.Produto)
                .HasForeignKey(o => o.IDFornecedor);

            HasRequired(o => o.UnidadeMedida)
                .WithMany(o=>o.Produto)
                .HasForeignKey(o => o.IDUnidadeMedida);
        }
    }
}
