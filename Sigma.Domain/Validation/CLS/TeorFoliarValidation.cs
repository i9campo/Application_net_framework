using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class TeorFoliarValidation : AbstractValidator<TeorFoliar>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public TeorFoliarValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(TeorFoliarReqMessage.objID);
            RuleFor(o => o.IDAmostraFoliar).NotEmpty().WithMessage(TeorFoliarReqMessage.IDAmostraFoliar);
            RuleFor(o => o.numero).NotEmpty().WithMessage(TeorFoliarReqMessage.numero); 
        }
    }
}
