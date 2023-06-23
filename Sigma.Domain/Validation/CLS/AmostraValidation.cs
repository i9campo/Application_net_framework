using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class AmostraValidation : AbstractValidator<Amostra>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public AmostraValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(AmostraReqMessage.objID);
            RuleFor(o => o.IDCultura).NotEmpty().WithMessage(AmostraReqMessage.IDCultura);
            RuleFor(o => o.descricao).NotEmpty().Length(1, 50).WithMessage(AmostraReqMessage.descricao);
            RuleFor(o => o.n).NotEmpty().WithMessage(AmostraReqMessage.n); 
            RuleFor(o => o.p2o5).NotEmpty().WithMessage(AmostraReqMessage.p2o5);
            RuleFor(o => o.k2o).NotEmpty().WithMessage(AmostraReqMessage.k2o);
            RuleFor(o => o.s).NotEmpty().WithMessage(AmostraReqMessage.s);
            RuleFor(o => o.ca).NotEmpty().WithMessage(AmostraReqMessage.ca);
            RuleFor(o => o.mg).NotEmpty().WithMessage(AmostraReqMessage.mg) ;
            RuleFor(o => o.b).NotEmpty().WithMessage(AmostraReqMessage.b);
            RuleFor(o => o.zn).NotEmpty().WithMessage(AmostraReqMessage.zn) ;
            RuleFor(o => o.cu).NotEmpty().WithMessage(AmostraReqMessage.cu);
            RuleFor(o => o.mn).NotEmpty().WithMessage(AmostraReqMessage.mn);
            RuleFor(o => o.co).NotEmpty().WithMessage(AmostraReqMessage.co);
            RuleFor(o => o.media).NotEmpty().WithMessage(AmostraReqMessage.media); 
        }
    }
}
