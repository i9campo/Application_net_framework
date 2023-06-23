using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IAmostraAppService : IAppService<Amostra>
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
