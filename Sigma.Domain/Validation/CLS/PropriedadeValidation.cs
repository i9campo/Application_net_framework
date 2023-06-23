using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class PropriedadeValidation : AbstractValidator<Propriedade>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public PropriedadeValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(PropriedadeReqMessage.objID);
            RuleFor(o => o.IDProprietario).NotEmpty().WithMessage(PropriedadeReqMessage.IDProprietario);
            RuleFor(o => o.nome).NotEmpty().Length(1,75).WithMessage(PropriedadeReqMessage.nome);
            //RuleFor(o => o.fone).NotEmpty().Length(1,17).WithMessage(PropriedadeReqMessage.fone);
        }
    }
}
