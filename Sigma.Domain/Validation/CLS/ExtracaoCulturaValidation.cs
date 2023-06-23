using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ExtracaoCulturaValidation : AbstractValidator<ExtracaoCultura>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ExtracaoCulturaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ExtracaoCulturaReqMessage.objID);
            RuleFor(o => o.IDCultura).NotEmpty().WithMessage(ExtracaoCulturaReqMessage.IDCultura);
            RuleFor(o => o.nutriente).NotEmpty().WithMessage(ExtracaoCulturaReqMessage.nutriente);

        }
    }
}
