using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ProblemaValidation : AbstractValidator<Problema>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ProblemaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ProblemaReqMessage.objID);
            RuleFor(o => o.IDArea).NotEmpty().WithMessage(ProblemaReqMessage.IDArea);
            RuleFor(o => o.tipo).NotEmpty().Length(1,50).WithMessage(ProblemaReqMessage.tipo);
            RuleFor(o => o.descricao).NotEmpty().Length(1,50).WithMessage(ProblemaReqMessage.descricao);
            RuleFor(o => o.nivel).NotEmpty().Length(1,50).WithMessage(ProblemaReqMessage.nivel);
            RuleFor(o => o.ano).NotEmpty().WithMessage(ProblemaReqMessage.ano);
        }
    }
}
