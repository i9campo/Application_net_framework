using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ImagemValidation : AbstractValidator<Imagem>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ImagemValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ImagemReqMessage.objID);
            RuleFor(o => o.IDAreaServico).NotEmpty().WithMessage(ImagemReqMessage.IDAreaServico);
            RuleFor(o => o.tipo).NotEmpty().Length(1,20).WithMessage(ImagemReqMessage.tipo);
            RuleFor(o => o.image).NotEmpty().WithMessage(ImagemReqMessage.image);
            RuleFor(o => o.indice).NotEmpty().WithMessage(ImagemReqMessage.indice);
            RuleFor(o => o.nome).NotEmpty().Length(1,100).WithMessage(ImagemReqMessage.nome);
            RuleFor(o => o.legenda1).NotEmpty().Length(1,50).WithMessage(ImagemReqMessage.legenda1);
            RuleFor(o => o.legenda2).NotEmpty().Length(1,50).WithMessage(ImagemReqMessage.legenda2);
            RuleFor(o => o.legenda3).NotEmpty().Length(1,50).WithMessage(ImagemReqMessage.legenda3);
            RuleFor(o => o.legenda4).NotEmpty().Length(1,50).WithMessage(ImagemReqMessage.legenda4);
            RuleFor(o => o.legenda5).NotEmpty().Length(1,50).WithMessage(ImagemReqMessage.legenda5);
            RuleFor(o => o.legenda6).NotEmpty().Length(1,50).WithMessage(ImagemReqMessage.legenda6);
            RuleFor(o => o.formatoGerado).NotEmpty().Length(1,2).WithMessage(ImagemReqMessage.formatoGerado);

        }
    }
}
