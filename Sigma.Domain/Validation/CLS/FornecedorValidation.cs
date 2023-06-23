using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public FornecedorValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(FornecedorReqMessage.objID);
            RuleFor(o => o.nome).NotEmpty().Length(1,50).WithMessage(FornecedorReqMessage.nome);
            //RuleFor(o => o.local).NotEmpty().Length(1, 100).WithMessage(FornecedorReqMessage.local);
            RuleFor(o => o.estado).NotEmpty().Length(1, 10).WithMessage(FornecedorReqMessage.estado);
            RuleFor(o => o.cidade).NotEmpty().Length(1, 50).WithMessage(FornecedorReqMessage.cidade); 
        }
    }
}
