using FluentValidation;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.Identity
{
    public class ClaimsValidation : AbstractValidator<Claims>
    {
        public ClaimsValidation()
        {
            RuleFor(c => c.objID).NotEmpty().WithMessage(ClaimsReqMessage.objID);
            RuleFor(c => c.ClaimType).NotEmpty().WithMessage(ClaimsReqMessage.ClaimType);
            RuleFor(c => c.ClaimValue).NotEmpty().WithMessage(ClaimsReqMessage.ClaimValue);
        }
    }
}
