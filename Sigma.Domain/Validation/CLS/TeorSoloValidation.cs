using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class TeorSoloValidation : AbstractValidator<TeorSolo>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public TeorSoloValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(TeorSoloReqMessage.objID);
            RuleFor(o => o.IDAmostraFoliar).NotEmpty().WithMessage(TeorSoloReqMessage.IDAmostraFoliar);
            RuleFor(o => o.n).NotEmpty().WithMessage(TeorSoloReqMessage.n);
            RuleFor(o => o.p).NotEmpty().WithMessage(TeorSoloReqMessage.p);
            RuleFor(o => o.k).NotEmpty().WithMessage(TeorSoloReqMessage.k);
            RuleFor(o => o.ca).NotEmpty().WithMessage(TeorSoloReqMessage.ca);
            RuleFor(o => o.mg).NotEmpty().WithMessage(TeorSoloReqMessage.mg);
            RuleFor(o => o.s).NotEmpty().WithMessage(TeorSoloReqMessage.s);
            RuleFor(o => o.b).NotEmpty().WithMessage(TeorSoloReqMessage.b);
            RuleFor(o => o.zn).NotEmpty().WithMessage(TeorSoloReqMessage.zn);
            RuleFor(o => o.mn).NotEmpty().WithMessage(TeorSoloReqMessage.mn);
            RuleFor(o => o.fe).NotEmpty().WithMessage(TeorSoloReqMessage.fe);
            RuleFor(o => o.cu).NotEmpty().WithMessage(TeorSoloReqMessage.cu);
        }
    }
}
