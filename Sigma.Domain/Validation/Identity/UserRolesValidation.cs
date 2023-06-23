using FluentValidation;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.Identity
{
    public class UserRolesValidation : AbstractValidator<UserRoles>
    {
        public UserRolesValidation()
        {
            RuleFor(o => o.UserId).NotEmpty().Length(1, 128).WithMessage(UserRolesReqMessage.UserId);
            RuleFor(o => o.RoleId).NotEmpty().Length(1, 128).WithMessage(UserRolesReqMessage.RoleId); 
        }
    }
}
