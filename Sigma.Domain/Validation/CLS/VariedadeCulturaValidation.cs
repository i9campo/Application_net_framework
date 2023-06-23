using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class VariedadeCulturaValidation : AbstractValidator<VariedadeCultura>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public VariedadeCulturaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(VariedadeCulturaReqMessage.objID);
            RuleFor(o => o.IDCultura).NotEmpty().WithMessage(VariedadeCulturaReqMessage.IDCultura);
            RuleFor(o => o.nome).NotEmpty().Length(1, 50).WithMessage(VariedadeCulturaReqMessage.nome);
            RuleFor(o => o.exigencia).Length(0,30).WithMessage(VariedadeCulturaReqMessage.exigencia);
            RuleFor(o => o.acamamento).Length(0, 10).WithMessage(VariedadeCulturaReqMessage.acamamento); 
        }
    }
}
