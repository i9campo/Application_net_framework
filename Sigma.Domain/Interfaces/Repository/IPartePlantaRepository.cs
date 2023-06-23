using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IPartePlantaRepository : IRepository<PartePlanta>
    {
        /// <summary>
        /// <para>Retorna uma lista de partes da planta a partir do ID da Cultura.</para>
        /// </summary>
        /// <param name="iDCultura"></param>
        /// <returns></returns>
        IEnumerable<PartePlanta> GetPartePlantaByCultura(Guid iDCultura);
    }
}
