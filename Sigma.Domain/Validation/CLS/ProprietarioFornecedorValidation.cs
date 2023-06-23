using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ProprietarioFornecedorValidation : AbstractValidator<ProprietarioFornecedor>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ProprietarioFornecedorValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ProprietarioFornecedorReqMessage.objID);
            RuleFor(o => o.IDProprietario).NotEmpty().WithMessage(ProprietarioFornecedorReqMessage.IDProprietario);
            RuleFor(o => o.IDFornecedor).NotEmpty().WithMessage(ProprietarioFornecedorReqMessage.IDFornecedor);
        }


    }
}
