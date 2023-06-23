using FluentValidation;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.Identity
{
    public class UsuarioAtivoValidation : AbstractValidator<UsuarioAtivo>
    {
        public UsuarioAtivoValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(UsuarioAtivoReqMessage.objID);
            RuleFor(o => o.IDEmpresa).NotEmpty().WithMessage(UsuarioAtivoReqMessage.IDEmpresa);
            RuleFor(o => o.IDUsuario).NotEmpty().WithMessage(UsuarioAtivoReqMessage.IDUsuario);
        }
    }
}
