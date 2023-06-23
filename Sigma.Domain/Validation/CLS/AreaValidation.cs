using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class AreaValidation : AbstractValidator<Area>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public AreaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(AreaReqMessage.objID); 
            //RuleFor(o => o.IDTipoArea).NotEmpty().WithMessage(AreaReqMessage.IDTipoArea);   
            RuleFor(o => o.IDPropriedade).NotEmpty().WithMessage(AreaReqMessage.IDPropriedade);
            RuleFor(o => o.nome).NotEmpty().Length(1, 50).WithMessage(AreaReqMessage.nome);

        }
    }
}
