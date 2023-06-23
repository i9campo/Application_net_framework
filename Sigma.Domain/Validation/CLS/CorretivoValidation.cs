using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class CorretivoValidation : AbstractValidator<Corretivo>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public CorretivoValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(CorretivoReqMessage.objID);
            RuleFor(o => o.IDAreaServico).NotEmpty().WithMessage(CorretivoReqMessage.IDAreaServico);
            RuleFor(x => x.descricao).NotEmpty().WithMessage(CorretivoReqMessage.descricao);
            //RuleFor(x => x.qtde).NotEmpty().WithMessage(CorretivoReqMessage.qtde);
            //RuleFor(x => x.prnt).NotEmpty().WithMessage(CorretivoReqMessage.prnt);
            //RuleFor(x => x.perCaO).NotEmpty().WithMessage(CorretivoReqMessage.perCaO);
            //RuleFor(x => x.perMgO).NotEmpty().WithMessage(CorretivoReqMessage.perMgO);
            //RuleFor(x => x.perP2O5).NotEmpty().WithMessage(CorretivoReqMessage.perP2O5);
            //RuleFor(x => x.perK2O).NotEmpty().WithMessage(CorretivoReqMessage.perK2O);
            //RuleFor(x => x.perCa).NotEmpty().WithMessage(CorretivoReqMessage.perCa);
            //RuleFor(x => x.perMg).NotEmpty().WithMessage(CorretivoReqMessage.perMg);
            //RuleFor(x => x.perS).NotEmpty().WithMessage(CorretivoReqMessage.perS);
            //RuleFor(x => x.s).NotEmpty().WithMessage(CorretivoReqMessage.s);
            //RuleFor(x => x.ca).NotEmpty().WithMessage(CorretivoReqMessage.ca);
            //RuleFor(x => x.mg).NotEmpty().WithMessage(CorretivoReqMessage.mg);
            //RuleFor(x => x.k).NotEmpty().WithMessage(CorretivoReqMessage.k);
            //RuleFor(x => x.p).NotEmpty().WithMessage(CorretivoReqMessage.p);
            //RuleFor(x => x.b).NotEmpty().WithMessage(CorretivoReqMessage.b);
            //RuleFor(x => x.zn).NotEmpty().WithMessage(CorretivoReqMessage.zn);
            //RuleFor(x => x.fe).NotEmpty().WithMessage(CorretivoReqMessage.fe);
            //RuleFor(x => x.mn).NotEmpty().WithMessage(CorretivoReqMessage.mn);
            //RuleFor(x => x.cu).NotEmpty().WithMessage(CorretivoReqMessage.cu);
            //RuleFor(x => x.co).NotEmpty().WithMessage(CorretivoReqMessage.co);
            //RuleFor(x => x.momicro).NotEmpty().WithMessage(CorretivoReqMessage.momicro);
            //RuleFor(x => x.v).NotEmpty().WithMessage(CorretivoReqMessage.v);
            //RuleFor(x => x.marcado).NotEmpty().WithMessage(CorretivoReqMessage.marcado);
            //RuleFor(x => x.opcaoMarcado).NotEmpty().WithMessage(CorretivoReqMessage.opcaoMarcado);
            //RuleFor(x => x.opcao).NotEmpty().WithMessage(CorretivoReqMessage.opcao);
            //RuleFor(x => x.custo).NotEmpty().WithMessage(CorretivoReqMessage.custo);
            //RuleFor(x => x.eficiencia).NotEmpty().WithMessage(CorretivoReqMessage.eficiencia);
            //RuleFor(x => x.perfil).NotEmpty().WithMessage(CorretivoReqMessage.perfil);
        }
    }
}
