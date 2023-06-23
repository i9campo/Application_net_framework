using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ImagemSateliteRecortadaValidation : AbstractValidator<ImagemSateliteRecortada>
    {
        public ImagemSateliteRecortadaValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ImagemSateliteRecorteMessage.objID);
            RuleFor(o => o.banda).NotEmpty().WithMessage(ImagemSateliteRecorteMessage.banda);
            RuleFor(o => o.satelite).NotEmpty().WithMessage(ImagemSateliteRecorteMessage.satelite);
            RuleFor(o => o.visualizar).NotEmpty().WithMessage(ImagemSateliteRecorteMessage.visualizar);
        }
    }
}
