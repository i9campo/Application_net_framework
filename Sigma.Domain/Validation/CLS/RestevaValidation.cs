using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class RestevaValidation: AbstractValidator<Resteva>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public RestevaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(RestevaReqMessage.objID);
            RuleFor(o => o.IDCultura).NotEmpty().WithMessage(RestevaReqMessage.IDCultura);
            RuleFor(o => o.tipo).NotEmpty().Length(1,2).WithMessage(RestevaReqMessage.tipo);
            RuleFor(o => o.descricao).NotEmpty().Length(1,30).WithMessage(RestevaReqMessage.descricao);
            RuleFor(o => o.media).NotEmpty().WithMessage(RestevaReqMessage.media);
        }
    }
}
 