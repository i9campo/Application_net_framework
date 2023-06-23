using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class UsoProdutoValidation : AbstractValidator<UsoProduto>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public UsoProdutoValidation()
        {
            RuleFor(c => c.objID).NotEmpty().WithMessage(UsoProdutoReqMessage.objID);
            RuleFor(c => c.IDProduto).NotEmpty().WithMessage(UsoProdutoReqMessage.IDProduto);
            RuleFor(c => c.IDCultura).NotEmpty().WithMessage(UsoProdutoReqMessage.IDCultura); 
        }
    }
}
