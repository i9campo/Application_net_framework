using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class PartePlantaValidation : AbstractValidator<PartePlanta>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public PartePlantaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(PartePlantaReqMessage.objID);
            RuleFor(o => o.IDCultura).NotEmpty().WithMessage(PartePlantaReqMessage.IDCultura);
            RuleFor(o => o.nome).NotEmpty().Length(1, 30).WithMessage(PartePlantaReqMessage.nome); 
        }
    }
}
