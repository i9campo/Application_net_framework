using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class NivelSoloValidation : AbstractValidator<NivelSolo>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public NivelSoloValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(NivelSoloReqMessage.objID);
            RuleFor(o => o.IDCultura).NotEmpty().WithMessage(NivelSoloReqMessage.IDCultura); 
        }
    }
}
