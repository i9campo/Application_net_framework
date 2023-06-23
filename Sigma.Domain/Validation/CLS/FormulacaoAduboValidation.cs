using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class FormulacaoAduboValidation : AbstractValidator<FormulacaoAdubo>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public FormulacaoAduboValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(FormulacaoAduboReqMessage.objID);
            RuleFor(o => o.IDAdubo).NotEmpty().WithMessage(FormulacaoAduboReqMessage.IDAdubo);
            RuleFor(o => o.descricao).NotEmpty().Length(1, 50).WithMessage(FormulacaoAduboReqMessage.descricao); 
        }
    }
}
