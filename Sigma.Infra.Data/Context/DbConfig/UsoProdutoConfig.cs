using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class UsoProdutoConfig : EntityTypeConfiguration<UsoProduto>
    {
        public UsoProdutoConfig()
        {

            HasRequired(o => o.Produto)
                .WithMany(o=>o.UsoProduto)
                .HasForeignKey(o => o.IDProduto);

            HasRequired(o => o.Cultura)
               .WithMany(o => o.UsoProduto)
               .HasForeignKey(o => o.IDCultura);

            Property(o => o.acao)
                .IsOptional();

            Property(o => o.classe)
                .IsOptional();

            Property(o => o.localAplicacao)
                .IsOptional();

            Property(o => o.alvo)
                .IsOptional();

            Property(o => o.doseBaixa)
                .IsOptional();

            Property(o => o.doseMedia)
                .IsOptional();

            Property(o => o.doseAlta)
                .IsOptional();

        }

    }
}
