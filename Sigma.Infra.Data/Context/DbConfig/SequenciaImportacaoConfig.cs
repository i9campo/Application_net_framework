using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class SequenciaImportacaoConfig : EntityTypeConfiguration<SequenciaImportacao>
    {
        public SequenciaImportacaoConfig()
        {
            HasRequired(o => o.Safra)
            .WithMany(o => o.SequenciaImportacaos)
            .HasForeignKey(o => o.IDSafra);

            HasRequired(o => o.Laboratorio)
            .WithMany(o => o.SequenciaImportacaos)
            .HasForeignKey(o => o.IDLaboratorio); 

            Property(o => o.NomeLaboratorio)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(25);

            Property(o => o.Grid)
            .IsOptional()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Area)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Nome)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.NPonto)
            .IsOptional()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.TpSolo)
            .IsOptional()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.Compac)
            .IsOptional()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.PHCaCl2)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.MO)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.PMeHl)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.PRes)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.K)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.S)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Ca)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Mg)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Al)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.CTC)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Argila)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.B)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Zn)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Fe)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Mn)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Cu)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Co)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.Momicro)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);

            Property(o => o.umphcacl2)
            .IsOptional()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.ummo)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umpmehl)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umpres)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umk2o)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);


            Property(o => o.ums)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umca)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.ummg)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umal)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);


            Property(o => o.umctc)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umargila)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umb)
           .IsRequired()
           .HasColumnType("varchar")
           .HasMaxLength(10);

            Property(o => o.umzn)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umfe)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.ummn)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umcu)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.umco)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);

            Property(o => o.ummomicro)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(10);
        }

    }
}
