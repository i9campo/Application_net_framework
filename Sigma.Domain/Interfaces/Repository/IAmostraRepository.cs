using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IAmostraRepository : IRepository<Amostra>
    {
        /// <summary>
        /// <para> Retorna as amostras a partir do ID da Cultura. </para>>
        /// <para> Adicional: Caso o retorno da amostra seja uma média a partir do ID da Cultura, informe um valor inteiro para mediaT ou caso não queira o retorno e só informar null. </para>
        /// </summary>
        /// <param name="IDCultura"></param>
        /// <returns></returns>
        IEnumerable<Amostra> GetByCultura(Guid IDCultura, int? mediaT);
    }
}
