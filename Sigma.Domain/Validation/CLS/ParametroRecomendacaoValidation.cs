using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ParametroRecomendacaoValidation: AbstractValidator<ParametroRecomendacao>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ParametroRecomendacaoValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ParametroRecomendacaoReqMessage.objID);
            RuleFor(o => o.IDSafra).NotEmpty().WithMessage(ParametroRecomendacaoReqMessage.IDSafra);
            RuleFor(o => o.IDArea).NotEmpty().WithMessage(ParametroRecomendacaoReqMessage.IDArea);
            RuleFor(o => o.tipo).NotEmpty().WithMessage(ParametroRecomendacaoReqMessage.tipo); 
        }
    }
}
