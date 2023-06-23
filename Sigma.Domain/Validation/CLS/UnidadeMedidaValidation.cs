using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class UnidadeMedidaValidation: AbstractValidator<UnidadeMedida>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public UnidadeMedidaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(UnidadeMedidaReqMessage.objID);
            RuleFor(o => o.nome).NotEmpty().Length(1, 150).WithMessage(UnidadeMedidaReqMessage.nome);
            RuleFor(o => o.peso).NotEmpty().WithMessage(UnidadeMedidaReqMessage.peso); 
        }
    }
}
