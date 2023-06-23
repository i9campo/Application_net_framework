using FluentValidation.Results;
using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IProdutoAppService : IAppService<Produto>
    {
        /// <summary>
        /// <para>Retorna todos os produtos, contendo informações como nome do fornecedor</para>
        /// <para>E nome da unidade de medida para preencher a Table.</para>
        /// <para>Necessario que o método seja uma View Exemplo (ProdutoView) </para>
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProdutoView> GetAllProduto();

        /// <summary>
        /// <para>Retorna uma lista de produto através do tipo de produto.</para>
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        IEnumerable<ProdutoView> GetByType(string tipo);
        IEnumerable<Produto> GetProdutoByName(string name);



        /// <summary>
        /// <para>Retorna um objeto contendo dados do Fornecedor e Unidade Medida</para>
        /// <para>Necessario que o método seja uma View Exemplo (ProdutoView)</para>
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        ProdutoView FindProduto(Guid objID);

        ValidationResult CadastroCorretivo(Produto produto);
        ValidationResult CadastroFertilizante(Produto produto);
        ValidationResult CadastroFoliar(Produto produto);
    }
}
