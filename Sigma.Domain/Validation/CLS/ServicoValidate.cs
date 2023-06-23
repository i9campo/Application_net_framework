using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ServicoValidate : AbstractValidator<Servico>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ServicoValidate()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ServicoReqMessage.objID);
            RuleFor(o => o.nome).NotEmpty().Length(1, 20).WithMessage(ServicoReqMessage.nome);
        }
    }
}
