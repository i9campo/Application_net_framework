
using FluentValidation;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.Identity
{
    public class UserClaimsValidation: AbstractValidator<UserClaims>
    {
        public UserClaimsValidation()
        {
            RuleFor(o => o.Id).NotEmpty().WithMessage(UserClaimsReqMessage.Id);
            RuleFor(o => o.UserId).NotEmpty().Length(1, 128).WithMessage(UserClaimsReqMessage.UserId); 
        }
    }
}
