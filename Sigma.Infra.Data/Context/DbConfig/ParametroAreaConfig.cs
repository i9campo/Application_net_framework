using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Context.MapConfig
{
    public class ParametroAreaConfig : EntityTypeConfiguration<ParametroArea> { 
    public ParametroAreaConfig()
    {
            HasRequired(o => o.AreaServico)
             .WithMany(o => o.ParametroArea)
             .HasForeignKey(o => o.IDAreaServico);

            HasOptional(o => o.Cultura)
            .WithMany(o => o.ParametroArea)
            .HasForeignKey(o => o.IDUltimaCultura);


            Property(o => o.areaArrendada)
            .IsOptional()
            .HasMaxLength(3)
            .HasColumnType("char");

            Property(o => o.tempoRestante)
            .IsOptional();

            Property(o => o.condicaoAtual)
            .HasMaxLength(30)
            .IsOptional();

            Property(o => o.tipoManejo)
            .HasMaxLength(30)
            .IsOptional();

            Property(o => o.fosfatoUtilizado)
            .HasMaxLength(30)
            .IsOptional();

            Property(o => o.anoAbertura)
            .IsOptional();

            Property(o => o.inicioPlantio)
            .HasColumnType("date")
            .IsOptional();

            Property(o => o.produtividadeArea)
           .IsOptional();

            Property(o => o.perfilSolo)
            .IsOptional()
            .HasMaxLength(3)
            .HasColumnType("char");

            Property(o => o.ultimaGessagem)
            .HasMaxLength(150)
            .IsOptional();

            Property(o => o.doseGessagem)
            .HasMaxLength(150)
            .IsOptional();

            Property(o => o.taxaAplicacaoGessagem)
            .HasMaxLength(30)
            .IsOptional();

            Property(o => o.CondAplicGessagem)
            .HasMaxLength(30)
            .IsOptional();


            Property(o => o.AnoCalagem)
             .HasMaxLength(5)
             .IsOptional();

            Property(o => o.correcaoSolo)
            .IsOptional()
            .HasMaxLength(3)
            .HasColumnType("char");

            Property(o => o.ultimaCalagem)
            .HasMaxLength(150)
            .IsOptional();

            Property(o => o.tipoCalcario)
            .HasMaxLength(30)
            .IsOptional();

            Property(o => o.doseCalagem)
            .HasMaxLength(50)
            .IsOptional();

            Property(o => o.taxaAplicacaoCalagem)
            .HasMaxLength(30)
            .IsOptional();

            Property(o => o.CondAplicCalagem)
            .HasMaxLength(30)
            .IsOptional();


            Property(o => o.equipamentoIncorporado)
            .HasMaxLength(15)
            .IsOptional();

            Property(o => o.observacao)
           .IsOptional()
           .HasColumnType("text");

            Property(o => o.efetuouFosfatagem)
            .IsOptional()
            .HasMaxLength(3)
            .HasColumnType("char");

            Property(o => o.anoUltimaFosfatagem)
             .HasMaxLength(150)
             .IsOptional();

            Property(o => o.formaAplicacao)
             .HasMaxLength(15)
             .IsOptional();

            Property(o => o.minaPreferencial)
           .HasMaxLength(50)
           .IsOptional();

            Property(o => o.dataInclusao)
            .HasColumnType("date")
            .IsOptional();

            Property(o => o.proximaCultura)
            .HasMaxLength(50)
            .IsOptional();

            Property(o => o.plantarHF)
            .HasMaxLength(5)
            .IsOptional();

            Property(o => o.utilizarGesso)
            .HasMaxLength(5)
            .IsOptional();

            Property(o => o.dosePrevistaGesso)
            .HasMaxLength(150)
            .IsOptional();

            Property(o => o.corrigirFosforo)
            .HasMaxLength(5)
            .IsOptional();

            Property(o => o.observacaoComplementar)
            .IsOptional()
            .HasColumnType("text");

            Property(o => o.v)
            .IsOptional();

            Property(o => o.recomendacaoFosforo)
           .IsOptional();

            Property(o => o.opcao)
           .IsOptional();

            Property(o => o.nematoideCisto)
           .IsOptional();

            Property(o => o.utilizarCalcario)
           .IsOptional();

            Property(o => o.utilizarGesso)
           .IsOptional();

            Property(o => o.utilizarFosforo)
           .IsOptional();

            Property(o => o.considerarResteva)
           .IsOptional();

        }
    }
}
