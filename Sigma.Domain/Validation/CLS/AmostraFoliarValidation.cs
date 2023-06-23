using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class AmostraFoliarValidation : AbstractValidator<AmostraFoliar>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public AmostraFoliarValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(AmostraFoliarReqMessage.objID);
            RuleFor(o => o.IDAreaServico).NotEmpty().WithMessage(AmostraFoliarReqMessage.IDAreaServico);
            RuleFor(o => o.IDEstagioCultura).NotEmpty().WithMessage(AmostraFoliarReqMessage.IDEstagioCultura);
            RuleFor(o => o.IDPartePlanta).NotEmpty().WithMessage(AmostraFoliarReqMessage.IDPartePlanta);
            RuleFor(o => o.data).NotEmpty().WithMessage(AmostraFoliarReqMessage.data);
            RuleFor(o => o.nome).NotEmpty().Length(1, 20).WithMessage(AmostraFoliarReqMessage.nome); 
        }
    }
}
