using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class LaboratorioValidation: AbstractValidator<Laboratorio>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public LaboratorioValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(LaboratorioReqMessage.objID);
            RuleFor(o => o.nome).NotEmpty().Length(1,50).WithMessage(LaboratorioReqMessage.nome);
            RuleFor(o => o.cnpj).NotEmpty().Length(18,18).WithMessage(LaboratorioReqMessage.cnpj);
            RuleFor(o => o.endereco).NotEmpty().Length(1,70).WithMessage(LaboratorioReqMessage.endereco);
            RuleFor(o => o.cep).NotEmpty().Length(9,10).WithMessage(LaboratorioReqMessage.cep);
            RuleFor(o => o.telefone).NotEmpty().Length(14,14).WithMessage(LaboratorioReqMessage.telefone);

        }
    }
}
