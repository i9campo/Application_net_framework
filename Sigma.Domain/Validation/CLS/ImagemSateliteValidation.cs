using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ImagemSateliteValidation : AbstractValidator<ImagemSatelite>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>

        public ImagemSateliteValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ImagemSateliteReqMessage.objID);
            RuleFor(o => o.banda).NotEmpty().WithMessage(ImagemSateliteReqMessage.banda);
            RuleFor(o => o.satelite).NotEmpty().WithMessage(ImagemSateliteReqMessage.satelite);
            RuleFor(o => o.visualizar).NotEmpty().WithMessage(ImagemSateliteReqMessage.visualizar);
        }
    }
}
