using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class ProdutoSimuladorConfig : EntityTypeConfiguration<ProdutoSimulador>
    {
        public ProdutoSimuladorConfig()
        {

            HasRequired(o => o.Simulacao)
                 .WithMany(o => o.ProdutoSimulador)
                 .HasForeignKey(o => o.IDSimulacao);

            HasRequired(o => o.Produto)
                .WithMany(o => o.ProdutoSimulador)
                .HasForeignKey(o => o.IDProduto);

            HasRequired(o => o.Cultura)
                 .WithMany(o => o.ProdutoSimulador)
                 .HasForeignKey(o => o.IDCultura);

            HasOptional(o => o.EstagioCultura)
                .WithMany(o => o.ProdutoSimulador)
                .HasForeignKey(o => o.IDEstagioCultura);

             HasRequired(o => o.Usuario)
            .WithMany(o => o.ProdutoSimulador)
            .HasForeignKey(o => o.IDUsuarioINC);

            HasOptional(o => o.Usuario)
            .WithMany(o => o.ProdutoSimulador)
            .HasForeignKey(o => o.IDUsuarioALT);

            Property(o => o.dateINC)
           .IsRequired();

            Property(o => o.dateALT)
            .IsOptional();

            Property(o => o.produto)
             .IsRequired();

            Property(o => o.doseMin)
             .IsRequired();

            Property(o => o.doseMax)
          .IsRequired();

            Property(o => o.dap)
          .IsOptional();

            Property(o => o.tipo)
         .IsRequired();





        }
    }
}
