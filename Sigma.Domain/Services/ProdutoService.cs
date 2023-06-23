using FluentValidation.Results;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class ProdutoService : Service<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public new ValidationResult Add(Produto produto)
        {
            throw new Exception();
        }

        public ValidationResult CadastroCorretivo(Produto produto)
        {
            produto.tipo = "CORRETIVO";
            //return base.Add(produto);

            return _repository.CadastroCorretivo(produto);
        }

        public ValidationResult CadastroFertilizante(Produto produto)
        {
            produto.tipo = "FERTILIZANTE";
            return _repository.CadastroFertilizante(produto);
            //return base.Add(produto);
        }

        public ValidationResult CadastroFoliar(Produto produto)
        {
            produto.tipo = "FOLIAR";
            return _repository.CadastroFoliar(produto);
            //return base.Add(produto);
        }

        public IEnumerable<Produto> GetProdutoByName(String Name)
        {
            return _repository.GetProdutoByName(Name);
        }


        public ProdutoView FindProduto(Guid objID)
        {
            return _repository.FindProduto(objID);
        }

        public IEnumerable<ProdutoView> GetAllProduto()
        {
            return _repository.GetAllProduto();
        }

        public IEnumerable<ProdutoView> GetByType(string tipo)
        {
            return _repository.GetByType(tipo);
        }
    }
}
