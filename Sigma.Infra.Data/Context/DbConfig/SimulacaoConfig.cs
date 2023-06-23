using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class SimulacaoConfig : EntityTypeConfiguration<Simulacao>
    {
        public SimulacaoConfig()
    {
              HasRequired(o => o.Cultura)
             .WithMany(o => o.Simulacao)
             .HasForeignKey(o => o.IDUltimaCultura);

            HasRequired(o => o.Cultura)
           .WithMany(o => o.Simulacao)
           .HasForeignKey(o => o.IDProximaCultura);

            HasRequired(o => o.AreaServico)
            .WithMany(o => o.Simulacao)
            .HasForeignKey(o => o.IDAreaServico);


            HasOptional(o => o.Usuario)
            .WithMany(o => o.Simulacao)
            .HasForeignKey(o => o.IDUsuarioALT);

            HasRequired(o => o.Usuario)
            .WithMany(o => o.Simulacao)
            .HasForeignKey(o => o.IDUsuarioINC);

            Property(o => o.dateINC)
             .IsRequired();

            Property(o => o.dateALT)
            .IsOptional();

            Property(o => o.opcao)
             .IsRequired();

            Property(o => o.nematoideCisto)
             .HasMaxLength(100)
                .IsRequired();

            Property(o => o.utilizarCalcario)
            .IsRequired();

            Property(o => o.utilizarGesso)
            .IsRequired();

            Property(o => o.utilizarFosforo)
            .IsRequired();

            Property(o => o.considerarResteva)
           .IsRequired();

            Property(o => o.metodoAcidezSuperficie)
          .IsRequired();

            Property(o => o.perVMinimoAcidezSuperf)
          .IsRequired();

            Property(o => o.mgMinimoAcidezSuperf)
           .IsRequired();

                Property(o => o.profundidadeAcidezSuperf)
              .IsRequired();

                Property(o => o.doseZeroAcidezSuperf)
           .IsRequired();

            Property(o => o.aplicouCorretivoAcidezSuperf)
          .IsRequired();

            Property(o => o.corrigirPerfilAcidezSuperf)
       .IsRequired();

            Property(o => o.perVacidezPerfil)
          .IsRequired();

            Property(o => o.profundidadeAcidezPerfil)
       .IsRequired();

            Property(o => o.utilizarCorretivoAcidezPerfil)
          .IsRequired();

            Property(o => o.metodoFosforo)
       .IsRequired();

            Property(o => o.fosforoDesejado)
          .IsRequired();

            Property(o => o.doseZeroFosforo)
       .IsRequired();

            Property(o => o.doseMediaFosforo)
          .IsRequired();

            Property(o => o.doseMediaEnxofre)
       .IsRequired();

            Property(o => o.baseFosforo)
          .IsRequired();

            Property(o => o.metodoEnxofre)
       .IsRequired();

            Property(o => o.enxofreDesejado)
          .IsRequired();

            Property(o => o.doseZeroEnxofre)
       .IsRequired();

            Property(o => o.corrigirPerfilEnxofre)
          .IsRequired();

            Property(o => o.metodoPotassio)
       .IsRequired();

            Property(o => o.potassioDesejado)
          .IsRequired();

            Property(o => o.doseZeroPotassio)
       .IsRequired();

            Property(o => o.doseMediaPotassio)
          .IsRequired();

            Property(o => o.paramRecomendFert)
        .IsRequired();

            Property(o => o.dataPlantioFert)
       .IsRequired();

            Property(o => o.produtividadeFert)
          .IsRequired();

        }

    }
}
