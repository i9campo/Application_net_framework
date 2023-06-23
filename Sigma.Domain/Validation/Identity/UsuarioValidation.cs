using FluentValidation;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.Identity
{
    public class UsuarioValidation: AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(o => o.Id).NotEmpty().Length(128).WithMessage(UsuarioReqMessage.Id);
            RuleFor(o => o.UserName).NotEmpty().Length(256).WithMessage(UsuarioReqMessage.UserName);
            RuleFor(o => o.Email).NotEmpty().Length(256).WithMessage(UsuarioReqMessage.Email);
            RuleFor(o => o.EmailConfirmed).NotEmpty().WithMessage(UsuarioReqMessage.EmailConfirmed);
            RuleFor(o => o.SecurityStamp).NotEmpty().WithMessage(UsuarioReqMessage.SecurityStamp);
            RuleFor(o => o.TwoFactorEnabled).NotEmpty().WithMessage(UsuarioReqMessage.TwoFactorEnabled);
            RuleFor(o => o.LockoutEnabled).NotEmpty().WithMessage(UsuarioReqMessage.LockoutEnabled);
            RuleFor(o => o.AccessFailedCount).NotEmpty().WithMessage(UsuarioReqMessage.AccessFailedCount); 
        }
    }
}
