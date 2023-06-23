using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class CulturaValidation : AbstractValidator<Cultura>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public CulturaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(CulturaReqMessage.objID);
            RuleFor(o => o.IDUnidadeMedida).NotEmpty().WithMessage(CulturaReqMessage.IDUnidadeMedida);
            RuleFor(o => o.nome).NotEmpty().Length(1,50).WithMessage(CulturaReqMessage.nome);
            
        }
    }
}
