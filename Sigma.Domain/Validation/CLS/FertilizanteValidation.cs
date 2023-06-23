using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class FertilizanteValidation : AbstractValidator<Fertilizante>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public FertilizanteValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(FertilizanteReqMessage.objID);
            RuleFor(o => o.IDCicloProducao).NotEmpty().WithMessage(FertilizanteReqMessage.IDCicloProducao);
            //RuleFor(o => o.foliar).NotEmpty().WithMessage(FertilizanteReqMessage.foliar);
            RuleFor(o => o.nome).NotEmpty().WithMessage(FertilizanteReqMessage.nome);
            //RuleFor(o => o.daedap).NotEmpty().WithMessage(FertilizanteReqMessage.daedap);
            //RuleFor(o => o.marcado).NotEmpty().WithMessage(FertilizanteReqMessage.marcado);
            //RuleFor(o => o.opcao).NotEmpty().WithMessage(FertilizanteReqMessage.opcao);
            //RuleFor(o => o.opcaoMarcada).NotEmpty().WithMessage(FertilizanteReqMessage.opcaoMarcada);
            //RuleFor(o => o.qtde).NotEmpty().WithMessage(FertilizanteReqMessage.qtde);
            //RuleFor(o => o.eficiencia).NotEmpty().WithMessage(FertilizanteReqMessage.eficiencia);
            //RuleFor(o => o.densidade).NotEmpty().WithMessage(FertilizanteReqMessage.densidade);
            //RuleFor(o => o.custo).NotEmpty().WithMessage(FertilizanteReqMessage.custo);
            //RuleFor(o => o.n).NotEmpty().WithMessage(FertilizanteReqMessage.n);
            //RuleFor(o => o.p2o5).NotEmpty().WithMessage(FertilizanteReqMessage.p2o5);
            //RuleFor(o => o.k2o).NotEmpty().WithMessage(FertilizanteReqMessage.k2o);
            //RuleFor(o => o.ca).NotEmpty().WithMessage(FertilizanteReqMessage.ca);
            //RuleFor(o => o.mg).NotEmpty().WithMessage(FertilizanteReqMessage.mg);
            //RuleFor(o => o.s).NotEmpty().WithMessage(FertilizanteReqMessage.s);
            //RuleFor(o => o.b).NotEmpty().WithMessage(FertilizanteReqMessage.b);
            //RuleFor(o => o.zn).NotEmpty().WithMessage(FertilizanteReqMessage.zn);
            //RuleFor(o => o.cu).NotEmpty().WithMessage(FertilizanteReqMessage.cu);
            //RuleFor(o => o.cu).NotEmpty().WithMessage(FertilizanteReqMessage.cu);
            //RuleFor(o => o.cu).NotEmpty().WithMessage(FertilizanteReqMessage.cu);
            //RuleFor(o => o.cu).NotEmpty().WithMessage(FertilizanteReqMessage.cu);
        }
    }
}
