using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class TipoAmostraValidate : AbstractValidator<TipoAmostra>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public TipoAmostraValidate()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(TipoAmostraReqMessage.objID);
            RuleFor(o => o.nome).NotEmpty().Length(1,100).WithMessage(TipoAmostraReqMessage.nome); 
        }
    }
}
