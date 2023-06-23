using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class SimulacaoValidate : AbstractValidator<Simulacao>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public SimulacaoValidate()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(SimulacaoReqMessage.objID);
            RuleFor(o => o.IDUltimaCultura).NotEmpty().WithMessage(SimulacaoReqMessage.IDUltimaCultura);
            RuleFor(o => o.IDProximaCultura).NotEmpty().WithMessage(SimulacaoReqMessage.IDProximaCultura);
            RuleFor(o => o.IDAreaServico).NotEmpty().WithMessage(SimulacaoReqMessage.IDAreaServico);
            RuleFor(o => o.IDUsuarioINC).NotEmpty().WithMessage(SimulacaoReqMessage.IDUsuarioINC);
            RuleFor(o => o.dateINC).NotEmpty().WithMessage(SimulacaoReqMessage.dateINC);
            RuleFor(o => o.opcao).NotEmpty().WithMessage(SimulacaoReqMessage.opcao);
            RuleFor(o => o.nematoideCisto).NotEmpty().Length(1,100).WithMessage(SimulacaoReqMessage.nematoideCisto);
            RuleFor(o => o.utilizarCalcario).NotEmpty().WithMessage(SimulacaoReqMessage.utilizarCalcario);
            RuleFor(o => o.utilizarGesso).NotEmpty().WithMessage(SimulacaoReqMessage.utilizarGesso);
            RuleFor(o => o.utilizarFosforo).NotEmpty().WithMessage(SimulacaoReqMessage.utilizarFosforo);
            RuleFor(o => o.considerarResteva).NotEmpty().WithMessage(SimulacaoReqMessage.considerarResteva);
            RuleFor(o => o.metodoAcidezSuperficie).NotEmpty().Length(1,100).WithMessage(SimulacaoReqMessage.metodoAcidezSuperficie);
            RuleFor(o => o.perVMinimoAcidezSuperf).NotEmpty().WithMessage(SimulacaoReqMessage.perVMinimoAcidezSuperf);
            RuleFor(o => o.mgMinimoAcidezSuperf).NotEmpty().WithMessage(SimulacaoReqMessage.mgMinimoAcidezSuperf);
            RuleFor(o => o.profundidadeAcidezSuperf).NotEmpty().WithMessage(SimulacaoReqMessage.profundidadeAcidezSuperf);
            RuleFor(o => o.doseZeroAcidezSuperf).NotEmpty().WithMessage(SimulacaoReqMessage.doseZeroAcidezSuperf);
            RuleFor(o => o.corrigirPerfilAcidezSuperf).NotEmpty().WithMessage(SimulacaoReqMessage.corrigirPerfilAcidezSuperf);
            RuleFor(o => o.perVacidezPerfil).NotEmpty().WithMessage(SimulacaoReqMessage.perVacidezPerfil);
            RuleFor(o => o.profundidadeAcidezPerfil).NotEmpty().WithMessage(SimulacaoReqMessage.profundidadeAcidezPerfil);
            RuleFor(o => o.utilizarCorretivoAcidezPerfil).NotEmpty().WithMessage(SimulacaoReqMessage.utilizarCorretivoAcidezPerfil);
            RuleFor(o => o.metodoFosforo).NotEmpty().Length(1,100).WithMessage(SimulacaoReqMessage.utilizarCorretivoAcidezPerfil);
            RuleFor(o => o.fosforoDesejado).NotEmpty().WithMessage(SimulacaoReqMessage.fosforoDesejado);
            RuleFor(o => o.doseZeroFosforo).NotEmpty().WithMessage(SimulacaoReqMessage.doseZeroFosforo);
            RuleFor(o => o.doseMediaFosforo).NotEmpty().WithMessage(SimulacaoReqMessage.doseMediaFosforo);
            RuleFor(o => o.baseFosforo).NotEmpty().WithMessage(SimulacaoReqMessage.baseFosforo);
            RuleFor(o => o.metodoEnxofre).NotEmpty().Length(1,100).WithMessage(SimulacaoReqMessage.metodoEnxofre);
            RuleFor(o => o.enxofreDesejado).NotEmpty().WithMessage(SimulacaoReqMessage.enxofreDesejado);
            RuleFor(o => o.doseZeroEnxofre).NotEmpty().WithMessage(SimulacaoReqMessage.doseZeroEnxofre);
            RuleFor(o => o.doseMediaEnxofre).NotEmpty().WithMessage(SimulacaoReqMessage.doseMediaEnxofre);
            RuleFor(o => o.corrigirPerfilEnxofre).NotEmpty().WithMessage(SimulacaoReqMessage.corrigirPerfilEnxofre);
            RuleFor(o => o.metodoPotassio).NotEmpty().WithMessage(SimulacaoReqMessage.metodoPotassio);
            RuleFor(o => o.potassioDesejado).NotEmpty().WithMessage(SimulacaoReqMessage.potassioDesejado);
            RuleFor(o => o.doseZeroPotassio).NotEmpty().WithMessage(SimulacaoReqMessage.doseZeroPotassio);
            RuleFor(o => o.doseMediaPotassio).NotEmpty().WithMessage(SimulacaoReqMessage.doseMediaPotassio);
            RuleFor(o => o.paramRecomendFert).NotEmpty().WithMessage(SimulacaoReqMessage.paramRecomendFert);
            RuleFor(o => o.dataPlantioFert).NotEmpty().WithMessage(SimulacaoReqMessage.dataPlantioFert);
            RuleFor(o => o.produtividadeFert).NotEmpty().WithMessage(SimulacaoReqMessage.produtividadeFert); 
        }
    }
}
