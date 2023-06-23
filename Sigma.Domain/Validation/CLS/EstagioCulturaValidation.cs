using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class EstagioCulturaValidation : AbstractValidator<EstagioCultura>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public EstagioCulturaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(EstagioCulturaReqMessage.objID);
            RuleFor(o => o.IDCultura).NotEmpty().WithMessage(EstagioCulturaReqMessage.IDCultura);
            RuleFor(o => o.codigo).NotEmpty().Length(1,50).WithMessage(EstagioCulturaReqMessage.codigo);
            RuleFor(o => o.descricao).NotEmpty().Length(1,70).WithMessage(EstagioCulturaReqMessage.descricao);
        }
    }
}
