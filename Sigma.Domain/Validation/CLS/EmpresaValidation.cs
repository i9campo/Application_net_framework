using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class EmpresaValidation : AbstractValidator<Empresa>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public EmpresaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(EmpresaReqMessage.objID);
            RuleFor(o => o.nome).NotEmpty().Length(1,100).WithMessage(EmpresaReqMessage.nome);
            RuleFor(o => o.dataCadastro).NotEmpty().WithMessage(EmpresaReqMessage.dataCadastro);
        }
    }
}
