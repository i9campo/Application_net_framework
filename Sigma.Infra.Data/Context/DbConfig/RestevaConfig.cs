using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{ 

    public class RestevaConfig : EntityTypeConfiguration<Resteva>
    {
        public RestevaConfig()
        {
            HasRequired(o => o.Cultura )
                    .WithMany(o=>o.Resteva)
                    .HasForeignKey(o => o.IDCultura);

            Property(o => o.descricao)
                    .IsRequired()
                    .HasMaxLength(30);

            Property(o => o.tipo)
                    .IsRequired()
                    .HasMaxLength(2);

            Property(o => o.media)
                    .IsRequired();

            Property(o => o.n)
                    .IsOptional();

            Property(o => o.p2o5)
                    .IsOptional();

            Property(o => o.s)
                    .IsOptional();

            Property(o => o.ca)
                    .IsOptional();

            Property(o => o.mg)
                    .IsOptional();

            Property(o => o.b)
                    .IsOptional();

            Property(o => o.zn)
                    .IsOptional();

            Property(o => o.cu)
                    .IsOptional();

            Property(o => o.co)
                    .IsOptional();

            Property(o => o.mo)
                .IsOptional();

            Property(o => o.mn)
                    .IsOptional();

            Property(o => o.k2o)
                    .IsOptional();

            Property(o => o.k)
                    .IsOptional();

            Property(o => o.p)
                    .IsOptional();
        }
    }
}
