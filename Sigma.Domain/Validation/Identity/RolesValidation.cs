using FluentValidation;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.Identity
{
    public class RolesValidation : AbstractValidator<Roles>
    {
        public RolesValidation()
        {
            RuleFor(o => o.Id).NotEmpty().Length(1,128).WithMessage(RolesReqMessage.Id);
            RuleFor(o => o.Name).NotEmpty().Length(1,256).WithMessage(RolesReqMessage.Name);
            RuleFor(o => o.ViewerRoler).NotEmpty().Length(1,256).WithMessage(RolesReqMessage.ViewerRoler); 
        }
    }
}
