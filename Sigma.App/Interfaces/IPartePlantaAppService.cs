using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IPartePlantaAppService : IAppService<PartePlanta>
    {
        /// <summary>
        /// <para>Retorna uma lista de partes da planta a partir do ID da Cultura.</para>
        /// </summary>
        /// <param name="iDCultura"></param>
        /// <returns></returns>
        IEnumerable<PartePlanta> GetPartePlantaByCultura(Guid iDCultura);
    }
}
