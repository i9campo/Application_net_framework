using FluentValidation;
using Sigma.Domain.Entities;
using Sigma.Domain.Validation.Messages;

namespace Sigma.Domain.Validation.CLS
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        /// <summary>
        /// Documentation for validation messages API. 
        /// https://fluentvalidation.net
        /// </summary>
        public ProdutoValidation()
        {
            RuleFor(o => o.objID).NotEmpty().WithMessage(ProdutoReqValidation.objID);
            RuleFor(o => o.IDFornecedor).NotEmpty().WithMessage(ProdutoReqValidation.IDFornecedor);
            RuleFor(o => o.IDUnidadeMedida).NotEmpty().WithMessage(ProdutoReqValidation.IDUnidadeMedida);
            RuleFor(o => o.nome).NotEmpty().Length(1,80).WithMessage(ProdutoReqValidation.nome);
            RuleFor(o => o.tipo).NotEmpty().Length(1,50).WithMessage(ProdutoReqValidation.tipo);
            //RuleFor(o => o.eficiencia).NotEmpty().WithMessage(ProdutoReqValidation.eficiencia);
            //RuleFor(o => o.densidade).NotEmpty().WithMessage(ProdutoReqValidation.densidade);
            //RuleFor(o => o.preco).NotEmpty().WithMessage(ProdutoReqValidation.preco);
            //RuleFor(o => o.prnt).NotEmpty().WithMessage(ProdutoReqValidation.prnt);
            //RuleFor(o => o.cao).NotEmpty().WithMessage(ProdutoReqValidation.cao);
            //RuleFor(o => o.mgo).NotEmpty().WithMessage(ProdutoReqValidation.mgo);
            //RuleFor(o => o.p2o5).NotEmpty().WithMessage(ProdutoReqValidation.p2o5);
            //RuleFor(o => o.k2o).NotEmpty().WithMessage(ProdutoReqValidation.k2o);
            //RuleFor(o => o.s).NotEmpty().WithMessage(ProdutoReqValidation.s);
            //RuleFor(o => o.n).NotEmpty().WithMessage(ProdutoReqValidation.n);
            //RuleFor(o => o.ca).NotEmpty().WithMessage(ProdutoReqValidation.ca);
            //RuleFor(o => o.mg).NotEmpty().WithMessage(ProdutoReqValidation.mg);
            //RuleFor(o => o.b).NotEmpty().WithMessage(ProdutoReqValidation.b);
            //RuleFor(o => o.zn).NotEmpty().WithMessage(ProdutoReqValidation.zn);
            //RuleFor(o => o.cu).NotEmpty().WithMessage(ProdutoReqValidation.cu);
            //RuleFor(o => o.mn).NotEmpty().WithMessage(ProdutoReqValidation.mn);
            //RuleFor(o => o.mo).NotEmpty().WithMessage(ProdutoReqValidation.mo);
            //RuleFor(o => o.co).NotEmpty().WithMessage(ProdutoReqValidation.co);
            //RuleFor(o => o.fe).NotEmpty().WithMessage(ProdutoReqValidation.fe);
            //RuleFor(o => o.si).NotEmpty().WithMessage(ProdutoReqValidation.si);
            //RuleFor(o => o.ni).NotEmpty().WithMessage(ProdutoReqValidation.ni); 


        }
    }
}
