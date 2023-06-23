using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class GridValidation : AbstractValidator<Grid>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public GridValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(GridReqMessage.objID);
            RuleFor(o => o.IDAreaServico).NotEmpty().WithMessage(GridReqMessage.IDAreaServico);
            RuleFor(o => o.descricao).NotEmpty().Length(1,50).WithMessage(GridReqMessage.descricao);
        }
    }
}
