using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class SequenciaImportacaoValidation : AbstractValidator<SequenciaImportacao>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public SequenciaImportacaoValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(SequenciaImportacaoReqMessage.objID);
            RuleFor(o => o.IDSafra).NotEmpty().WithMessage(SequenciaImportacaoReqMessage.IDSafra);
            RuleFor(o => o.Area).NotEmpty().Length(0,2).WithMessage(SequenciaImportacaoReqMessage.Area);
            RuleFor(o => o.Nome).NotEmpty().WithMessage(SequenciaImportacaoReqMessage.Nome);
            //RuleFor(o => o.PHCaCl2).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.PHCaCl2);
            //RuleFor(o => o.MO).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.MO);
            //RuleFor(o => o.PMeHl).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.PMeHl);
            //RuleFor(o => o.PRes).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.PRes);
            //RuleFor(o => o.K).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.K);
            //RuleFor(o => o.S).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.S);
            //RuleFor(o => o.Ca).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Ca);
            //RuleFor(o => o.Mg).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Mg);
            //RuleFor(o => o.Al).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Al); 
            //RuleFor(o => o.CTC).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.CTC);
            //RuleFor(o => o.Argila).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Argila);
            //RuleFor(o => o.B).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.B);
            //RuleFor(o => o.Zn).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Zn);
            //RuleFor(o => o.Fe).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Fe);
            //RuleFor(o => o.Mn).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Mn);
            //RuleFor(o => o.Cu).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Cu);
            //RuleFor(o => o.Co).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Co);
            //RuleFor(o => o.Momicro).Empty().Length(0, 2).WithMessage(SequenciaImportacaoReqMessage.Momicro);
            //RuleFor(o => o.umphcacl2).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umphcacl2);
            //RuleFor(o => o.ummo).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.ummo);
            //RuleFor(o => o.umpmehl).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umpmehl);
            //RuleFor(o => o.umpres).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umpres);
            //RuleFor(o => o.umk2o).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umk2o);
            //RuleFor(o => o.ums).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.ums);
            //RuleFor(o => o.umca).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umca);
            //RuleFor(o => o.ummg).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.ummg);
            //RuleFor(o => o.umal).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umal);
            //RuleFor(o => o.umctc).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umctc);
            //RuleFor(o => o.umargila).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umargila);
            //RuleFor(o => o.umb).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umb);
            //RuleFor(o => o.umzn).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umzn);
            //RuleFor(o => o.umfe).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umfe);
            //RuleFor(o => o.ummn).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.ummn);
            //RuleFor(o => o.umcu).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umcu);
            //RuleFor(o => o.umco).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.umco);
            //RuleFor(o => o.ummomicro).Empty().Length(0, 10).WithMessage(SequenciaImportacaoReqMessage.ummomicro);




        }
    }
}
