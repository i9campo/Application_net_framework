using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public  class CicloProducaoValidation : AbstractValidator<CicloProducao>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public CicloProducaoValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(CicloProducaoReqMessage.objID);
            RuleFor(o => o.IDAreaServico).NotEmpty().WithMessage(CicloProducaoReqMessage.IDAreaServico);
            RuleFor(o => o.tipo).NotEmpty().Length(1, 2).WithMessage(CicloProducaoReqMessage.tipo);
            //RuleFor(o => o.ciclo).NotEmpty().WithMessage(CicloProducaoReqMessage.ciclo); 
            RuleFor(o => o.identificacao).NotEmpty().Length(1, 100).WithMessage(CicloProducaoReqMessage.identificacao);
            //RuleFor(o => o.codigo).NotEmpty().WithMessage(CicloProducaoReqMessage.codigo); 
        }
    }
}
