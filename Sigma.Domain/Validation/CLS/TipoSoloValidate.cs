using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class TipoSoloValidate : AbstractValidator<TipoSolo>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public TipoSoloValidate()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(TipoSoloReqMessage.objID);
            RuleFor(o => o.abreviacao).NotEmpty().Length(1, 5).WithMessage(TipoSoloReqMessage.abreviacao);
            RuleFor(o => o.descricao).NotEmpty().Length(1, 20).WithMessage(TipoSoloReqMessage.descricao); 
        }
    }
}
