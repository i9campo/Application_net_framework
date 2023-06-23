using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IPartePlantaService : IService<PartePlanta>
    {
        /// <summary>
        /// <para>Retorna uma lista de partes da planta a partir do ID da Cultura.</para>
        /// </summary>
        /// <param name="iDCultura"></param>
        /// <returns></returns>
        IEnumerable<PartePlanta> GetPartePlantaByCultura(Guid iDCultura);
    }
}
