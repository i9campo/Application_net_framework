using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ProprietarioValidation : AbstractValidator<Proprietario>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ProprietarioValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ProprietarioReqMessage.objID);
            RuleFor(o => o.nome).NotEmpty().Length(1, 75).WithMessage(ProprietarioReqMessage.nome); 
        }
    }
}
