using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class SafraValidation : AbstractValidator<Safra>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public SafraValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(SafraReqMessage.objID);
            RuleFor(o => o.descricao).NotEmpty().Length(1, 25).WithMessage(SafraReqMessage.descricao); 
        }
    }
}
