using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ProdutividadeVariedadeValidation : AbstractValidator<ProdutividadeVariedade>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ProdutividadeVariedadeValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ProdutividadeVariedadeReqMessage.objID);
            RuleFor(o => o.IDRegiao).NotEmpty().WithMessage(ProdutividadeVariedadeReqMessage.IDRegiao);
            RuleFor(o => o.IDVariedadeCultura).NotEmpty().WithMessage(ProdutividadeVariedadeReqMessage.IDVariedadeCultura);
            RuleFor(o => o.IDUnidadeMedida).NotEmpty().WithMessage(ProdutividadeVariedadeReqMessage.IDUnidadeMedida);
            RuleFor(o => o.qtdeProduzida).NotEmpty().WithMessage(ProdutividadeVariedadeReqMessage.qtdeProduzida);
            RuleFor(o => o.ciclo).NotEmpty().Length(1,15).WithMessage(ProdutividadeVariedadeReqMessage.ciclo);
            RuleFor(o => o.ano).NotEmpty().WithMessage(ProdutividadeVariedadeReqMessage.ano); 
        }
    }
}
