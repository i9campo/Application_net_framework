using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class RegiaoValidation : AbstractValidator<Regiao>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public RegiaoValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(RegiaoReqMessage.objID);
            RuleFor(o => o.descricao).NotEmpty().Length(1, 50).WithMessage(RegiaoReqMessage.descricao); 
        }
    }
}
