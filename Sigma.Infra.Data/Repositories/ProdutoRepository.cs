using FluentValidation.Results;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public new ValidationResult Add(Produto produto)
        {
            throw new Exception();
        }

        public ValidationResult CadastroCorretivo(Produto produto)
        {
            produto.tipo = "CORRETIVO";
            return base.Add(produto);
        }

        public ValidationResult CadastroFertilizante(Produto produto)
        {
            produto.tipo = "FERTILIZANTE";
            return base.Add(produto);

        }

        public ValidationResult CadastroFoliar(Produto produto)
        {
            produto.tipo = "FOLIAR";
            return base.Add(produto);
        }

        public IEnumerable<Produto> GetProdutoByName(String Name)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT fd.nome as fornecedor, um.nome as unidademedida,  Produto.* FROM Produto ");
            query.AppendLine("INNER JOIN UnidadeMedida um On um.objID = Produto.IDUnidadeMedida ");
            query.AppendLine("INNER JOIN Fornecedor fd On fd.objID = Produto.IDFornecedor ");
            query.AppendLine("WHERE Produto.nome = '" + Name + "'");
            return Context.Database.SqlQuery<Produto>(query.ToString()).ToList();
        }


        public ProdutoView FindProduto(Guid objID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT fd.nome as fornecedor, um.nome as unidademedida,  Produto.* FROM Produto ");
            query.AppendLine("INNER JOIN UnidadeMedida um On um.objID = Produto.IDUnidadeMedida ");
            query.AppendLine("INNER JOIN Fornecedor fd On fd.objID = Produto.IDFornecedor ");
            query.AppendLine("WHERE Produto.objID = '" + objID.ToString() + "'");
            return Context.Database.SqlQuery<ProdutoView>(query.ToString()).SingleOrDefault();
        }

        public IEnumerable<ProdutoView> GetAllProduto()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT fd.nome as fornecedor, um.nome as unidademedida,  Produto.* FROM Produto ");
            query.AppendLine("INNER JOIN UnidadeMedida um On um.objID = Produto.IDUnidadeMedida ");
            query.AppendLine("INNER JOIN Fornecedor fd On fd.objID = Produto.IDFornecedor ");
            return Context.Database.SqlQuery<ProdutoView>(query.ToString());
        }

        public IEnumerable<ProdutoView> GetByType(string tipo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT f.nome as fornecedor, um.nome as unidademedida,  Produto.* FROM Produto ");
            query.AppendLine("INNER JOIN UnidadeMedida um On um.objID = Produto.IDUnidadeMedida ");
            query.AppendLine("INNER JOIN Fornecedor f On f.objID = Produto.IDFornecedor ");
            query.AppendLine("WHERE Produto.tipo = '" + tipo + "' ORDER BY Produto.nome desc, Produto.ativo");
            return Context.Database.SqlQuery<ProdutoView>(query.ToString());
        }
    }
}
