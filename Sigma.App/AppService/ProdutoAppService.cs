using FluentValidation.Results;
using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class ProdutoAppService : AppService<Produto>, IProdutoAppService
    {
        private readonly IProdutoService _Service;
        public ProdutoAppService(IProdutoService service)
            :base(service)
        {
            _Service = service; 
        }

        public new ValidationResult Add(Produto produto)
        {
            throw new Exception();
        }

        public ValidationResult CadastroCorretivo(Produto produto)
        {
            produto.tipo = "CORRETIVO";
            return _Service.CadastroCorretivo(produto);
            //return base.Add(produto);
        }

        public ValidationResult CadastroFertilizante(Produto produto)
        {
            produto.tipo = "FERTILIZANTE";
            return _Service.CadastroFertilizante(produto);
            //return base.Add(produto); 
        }

        public ValidationResult CadastroFoliar(Produto produto)
        {
            produto.tipo = "FOLIAR";
            return _Service.CadastroFoliar(produto);
            //return base.Add(produto);
        }

        public ProdutoView FindProduto(Guid objID)
        {
            return _Service.FindProduto(objID);
        }

        public IEnumerable<ProdutoView> GetAllProduto()
        {
            return _Service.GetAllProduto();
        }

        public IEnumerable<ProdutoView> GetByType(string tipo)
        {
            return _Service.GetByType(tipo);
        }

        public IEnumerable<Produto> GetProdutoByName(String Name)
        {
            return _Service.GetProdutoByName(Name);
        }

    }
}
