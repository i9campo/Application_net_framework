using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ParametroPropriedadeValidation : AbstractValidator<ParametroPropriedade>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ParametroPropriedadeValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ParametroPropriedadeReqMessage.objID);
            RuleFor(o => o.IDSafra).NotEmpty().WithMessage(ParametroPropriedadeReqMessage.IDSafra);
            RuleFor(o => o.IDPropriedade).NotEmpty().WithMessage(ParametroPropriedadeReqMessage.IDPropriedade); 
        }
    }
}
