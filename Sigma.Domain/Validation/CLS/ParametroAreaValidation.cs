using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ParametroAreaValidation : AbstractValidator<ParametroArea>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ParametroAreaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ParametroAreaReqMessage.objID);
            RuleFor(o => o.IDAreaServico).NotEmpty().WithMessage(ParametroAreaReqMessage.IDAreaServico); 
        }
    }
}
