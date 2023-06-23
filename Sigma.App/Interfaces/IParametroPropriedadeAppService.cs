using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IParametroPropriedadeAppService : IAppService<ParametroPropriedade>
    {
        IEnumerable<ParametroPropriedade> FindParametroPropriedade(Guid objID);

        ParametroSoloView GetSolo(Guid IDAreaServico);

        /// <summary>
        /// <para>Retorna o parâmetro da propriedade a partir da safra e propriedade. </para>
        /// </summary>
        /// <param name="IDSafra">Recebe o valor do objID da safra.</param>
        /// <param name="IDPropriedade">Recebe o valor do objID da Propriedade.</param>
        /// <returns></returns>
        ParametroPropriedade GetByAreaPropriedade(Guid IDSafra, Guid IDPropriedade);

    }
}
