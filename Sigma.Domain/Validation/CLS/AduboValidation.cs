using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class AduboValidation : AbstractValidator<Adubo>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public AduboValidation()
        {
            RuleFor(x => x.objID).NotEmpty().WithMessage(AduboReqMessage.objID);
            RuleFor(x => x.descricao).NotEmpty().Length(1, 50).WithMessage(AduboReqMessage.descricao); 
        }
    }
}
