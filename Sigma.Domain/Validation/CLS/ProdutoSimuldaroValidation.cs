using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ProdutoSimuldaroValidation : AbstractValidator<ProdutoSimulador>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ProdutoSimuldaroValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ProdutoSimuladoReqMessage.objID);
            RuleFor(o => o.IDSimulacao).NotEmpty().WithMessage(ProdutoSimuladoReqMessage.IDSimulacao);
            RuleFor(o => o.IDProduto).NotEmpty().WithMessage(ProdutoSimuladoReqMessage.IDProduto);
            RuleFor(o => o.dateINC).NotEmpty().WithMessage(ProdutoSimuladoReqMessage.dateINC);
            RuleFor(o => o.produto).NotEmpty().Length(1,200).WithMessage(ProdutoSimuladoReqMessage.produto);
            RuleFor(o => o.doseMin).NotEmpty().WithMessage(ProdutoSimuladoReqMessage.doseMin);
            RuleFor(o => o.doseMax).NotEmpty().WithMessage(ProdutoSimuladoReqMessage.doseMax);
            RuleFor(o => o.tipo).NotEmpty().WithMessage(ProdutoSimuladoReqMessage.tipo);

        }
    }
}
