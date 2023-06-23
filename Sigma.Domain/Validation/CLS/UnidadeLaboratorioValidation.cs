using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class UnidadeLaboratorioValidation : AbstractValidator<UnidadeDeLaboratorio>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public UnidadeLaboratorioValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(UnidadeLaboratorioReqMessage.objID);
            RuleFor(o => o.IDLaboratorio).NotEmpty().WithMessage(UnidadeLaboratorioReqMessage.IDLaboratorio); 
        }
    }
}
