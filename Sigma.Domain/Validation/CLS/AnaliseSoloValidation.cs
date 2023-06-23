using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class AnaliseSoloValidation : AbstractValidator<AnaliseSolo>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public AnaliseSoloValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(AnaliseSoloReqMessage.objID); 
            RuleFor(o => o.IDAreaServico).NotEmpty().WithMessage(AnaliseSoloReqMessage.IDAreaServico);
            RuleFor(o => o.descricao).NotEmpty().Length(1, 200).WithMessage(AnaliseSoloReqMessage.descrição);
        }
    }
}
