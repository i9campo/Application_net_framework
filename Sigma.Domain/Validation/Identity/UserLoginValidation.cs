using FluentValidation;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.Identity
{
    public class UserLoginValidation : AbstractValidator<UserLogin>
    {
        public UserLoginValidation()
        {
            RuleFor(o => o.UserId).NotEmpty().Length(1,128).WithMessage(UserLoginReqMessage.UserId);
            RuleFor(o => o.LoginProvider).NotEmpty().Length(1, 128).WithMessage(UserLoginReqMessage.LoginProvider);
            RuleFor(o => o.ProviderKey).NotEmpty().Length(1, 128).WithMessage(UserLoginReqMessage.ProviderKey); 
        }
    }
}
