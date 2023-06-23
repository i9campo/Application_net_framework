using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class FaixaTeorValidation: AbstractValidator<FaixaTeor>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public FaixaTeorValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(FaixaTeorReqMessage.objID);
            RuleFor(o => o.IDEstagioCultura).NotEmpty().WithMessage(FaixaTeorReqMessage.IDEstagioCultura);
            RuleFor(o => o.IDPartePlanta).NotEmpty().WithMessage(FaixaTeorReqMessage.IDPartePlanta);
            RuleFor(o => o.nutriente).NotEmpty().Length(1, 2).WithMessage(FaixaTeorReqMessage.nutriente); 
        }
    }
}
