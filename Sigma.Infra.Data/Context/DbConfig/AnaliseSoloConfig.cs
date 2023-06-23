using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class AnaliseSoloConfig : EntityTypeConfiguration<AnaliseSolo>
    {
        public AnaliseSoloConfig()
        {
            HasRequired(o => o.AreaServico)
                .WithMany(o=>o.AnaliseSolo)
                .HasForeignKey(o => o.IDAreaServico);

            HasOptional(o => o.TipoSolo)
                .WithMany(o=>o.AnaliseSolo)
                .HasForeignKey(o => o.IDTipoSolo);

            HasOptional(o => o.Grid)
                .WithMany(o => o.AnaliseSolo)
                .HasForeignKey(o => o.IDGrid);

            Property(o => o.descricao)
               .HasMaxLength(200)
               .IsOptional();

            Property(o => o.data)
                .IsOptional()
                .HasColumnType("date");

            Property(o => o.compactacao)
               .HasMaxLength(10)
               .IsOptional()
               .HasColumnType("nvarchar");

            Property(o => o.profundidade)
                .HasMaxLength(10)
                .IsOptional()
                .HasColumnType("nvarchar");

            Property(o => o.ponto)
               .IsRequired()
               .HasColumnType("int");

            Property(o => o.subAmostra)
               .IsRequired()
               .HasColumnType("bit");

            Property(o => o.sequenciaSubA)
              .IsOptional()
              .HasMaxLength(10)
              .HasColumnType("nvarchar");

            Property(o => o.agua)
              .IsOptional()
              .HasColumnType("float");

            Property(o => o.cacl2)
              .IsOptional()
              .HasColumnType("float");

            Property(o => o.mo)
              .IsOptional()
              .HasColumnType("float");

            Property(o => o.momicro)
              .IsOptional()
              .HasColumnType("float");

            Property(o => o.co)
              .IsOptional()
              .HasColumnType("float");

            Property(o => o.pmehl)
              .IsOptional()
              .HasColumnType("float");

            Property(o => o.pres)
              .IsOptional()
              .HasColumnType("float");

            Property(o => o.k)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.s)
            .IsOptional()
            .HasColumnType("float");


            Property(o => o.ca)
            .IsOptional()
            .HasColumnType("float");

            Property(o => o.mg)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.al)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.hal)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.ctc)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.argila)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.b)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.zn)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.fe)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.mn)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.somaBase)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.v)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.relcamg)
                .IsOptional()
                .HasColumnType("float");


            Property(o => o.relcak)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.relmgk)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.relcamgk)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.ctcca)
                .IsOptional()
                .HasColumnType("float");

            Property(o => o.ctcmg)
                .IsOptional()
                .HasColumnType("float");
            
            Property(o => o.ctck)
                .IsOptional()
                .HasColumnType("float");
            
            Property(o => o.ctcal)
                .IsOptional()
                .HasColumnType("float");
            
            Property(o => o.geo)
                .IsOptional()
                .HasColumnType("geography");


            Property(o => o.jsonField)
                .IsOptional(); 
        }
    }
}
