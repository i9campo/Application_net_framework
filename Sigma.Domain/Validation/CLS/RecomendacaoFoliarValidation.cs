using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class RecomendacaoFoliarValidation: AbstractValidator<RecomendacaoFoliar>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public RecomendacaoFoliarValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(RecomendacaoFoliarReqMessage.objID);
            RuleFor(o => o.IDCultura).NotEmpty().WithMessage(RecomendacaoFoliarReqMessage.IDCultura);
            RuleFor(o => o.codigo).NotEmpty().Length(1,26).WithMessage(RecomendacaoFoliarReqMessage.codigo);

        }
    }
}
